using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Delex_POS.Application.Users.Queries.GetUsers;
using Delex_POS.Infrastructure.Factories;

namespace Delex_POS.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TokenFactory _tokenFactory;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        IMapper mapper,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        TokenFactory tokenFactory,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _mapper = mapper;
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenFactory = tokenFactory;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }
#region User Management
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<ApplicationUserDto> GetUserById(string userid)
    {
        var user = await _userManager.FindByIdAsync(userid);        
        Guard.Against.NotFound(userid, user);
        return _mapper.Map<ApplicationUserDto>(user);
    }

    public IQueryable<ApplicationUserDto> GetAllUsers(string? searchQuery, string sortBy, TableSort sort)
    {
        Expression<Func<ApplicationUser, bool>> filter = searchQuery switch
        {
            (null) => x => true,
            (_) => x => x.FirstName.Contains(searchQuery)
                        || x.LastName.Contains(searchQuery)
                        || x.Email!.Contains(searchQuery),
        };

        IQueryable<ApplicationUser> users = _userManager.Users.AsNoTracking()
            .Where(filter);

        users = users.OrderBy(sortBy, sort);

        return users.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider);
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(ApplicationUserDto user, string password)
    {
        var newUser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
        };
        newUser.Update(
            lastName: user.LastName,
            firstName: user.FirstName,
            userCode: user.UserCode,
            middleName: user.MiddleName
        );
        newUser.SetBranch(user.BranchId);

        var result = await _userManager.CreateAsync(newUser, password);

        return (result.ToApplicationResult(), newUser.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    private async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<Result> UpdateUserAsync(ApplicationUserDto user)
    {
        var entity = await _userManager.FindByIdAsync(user.Id);
        Guard.Against.NotFound(user.Id, entity);
        entity.Update(
            userCode: user.UserCode,
            lastName: user.LastName,
            firstName: user.FirstName,
            middleName: user.MiddleName
            );
        entity.SetBranch(user.BranchId);
        return entity != null ? await UpdateUserAsync(entity) : Result.Success();
    }
    
    private async Task<Result> UpdateUserAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.ToApplicationResult();
    }
#endregion

#region Role Management
    public async Task<(Result Result, string RoleId)> CreateRoleAsync(string roleName)
    {
        var role = new IdentityRole
        {
            Name = roleName,
            NormalizedName = roleName.ToUpperInvariant(),
        };

        var result = await _roleManager.CreateAsync(role);

        return (result.ToApplicationResult(), role.Id);
    }
    
    public async Task<Result> DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        return role != null ? await DeleteRoleAsync(role) : Result.Success();
    }

    public async Task<Result> DeleteRoleAsync(IdentityRole role)
    {
        var result = await _roleManager.DeleteAsync(role);

        return result.ToApplicationResult();
    }

    public async Task<Result> AddToRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure(new[] { "User not found." });

        // Ensure role exists before assigning
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }

        var result = await _userManager.AddToRoleAsync(user, role);
        return result.ToApplicationResult();
    }

    public async Task<Result> RemoveFromRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure(new[] { "User not found." });

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        return result.ToApplicationResult();
    }

    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return new List<string>();

        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> RoleExistAsync(string roleName)
    {
        return await _roleManager.Roles.AnyAsync(v => v.Name == roleName);
    }

    public async Task<IList<IdentityRoleDto>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        IQueryable<IdentityRole> roles = _roleManager.Roles.AsNoTracking();

        IQueryable<IdentityRoleDto> projectedRoles = roles.ProjectTo<IdentityRoleDto>(_mapper.ConfigurationProvider);

        return await projectedRoles.ToListAsync(cancellationToken);
    }

    public IQueryable<IdentityRoleDto> GetAllRoles(string? searchQuery, string sortBy, TableSort sort)
    {
        Expression<Func<IdentityRole, bool>> filter = searchQuery switch
        {
            (null) => x => true,
            (_) => x => x.Name!.Contains(searchQuery),
        };

        IQueryable<IdentityRole> roles = _roleManager.Roles
            .AsNoTracking()
            .Where(filter);
        
        roles = roles.OrderBy(sortBy, sort);

        return roles.ProjectTo<IdentityRoleDto>(_mapper.ConfigurationProvider);
    }

    public async Task<IdentityRoleDto> GetRoleByIdAsync(string id, CancellationToken cancellationToken)
    {
        IdentityRole role = await _roleManager.Roles.AsNoTracking().FirstAsync(v => v.Id == id);
        Guard.Against.NotFound(id, role);
        return _mapper.Map<IdentityRoleDto>(role);
    }
    
    public async Task<IdentityRoleDto> GetRoleByNameAsync(string name, CancellationToken cancellationToken)
    {
        IdentityRole role = await _roleManager.Roles.AsNoTracking().FirstAsync(v => v.Name == name);
        Guard.Against.NotFound(name, role);
        return _mapper.Map<IdentityRoleDto>(role);
    }
    #endregion

#region Login Management
    public async Task<TokenResponse?> LoginAsync(string userCode, string password, bool isPersistent = false)
    {

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserCode.Equals(userCode));

        if (user is null)
        {
            return null;
        }

        user = await _userManager.FindByEmailAsync(user.Email!);
        if (user is null)
        {
            return null;
        }

        var signInResult = await _signInManager.PasswordSignInAsync(
            user,
            password,
            isPersistent,
            lockoutOnFailure: true);

        if (!signInResult.Succeeded)
        {
            return null;
        }

        const int expiresInMinutes = 60;

        return new TokenResponse
        {
            TokenType = null,
            AccessToken = _tokenFactory.GenerateAccessToken(user, expiresInMinutes),
            ExpiresIn = expiresInMinutes * 60,
            RefreshToken = _tokenFactory.GenerateRefreshToken()
        };
    }
#endregion
}
