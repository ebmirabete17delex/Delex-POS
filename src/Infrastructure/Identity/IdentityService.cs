using System.Linq.Expressions;
using Delex_POS.Application.Common.Enums;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Models;
using Delex_POS.Application.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Delex_POS.Application.Roles.Queries.RoleDTOs;
using Delex_POS.Infrastructure.Factories;
using Delex_POS.Application.Employees.Queries.EmployeeDTOs;

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

    public async Task<ApplicationUserDto> GetUserByIdAsync(string userid)
    {
        var user = await _userManager.FindByIdAsync(userid);        
        Guard.Against.NotFound(userid, user);
        return _mapper.Map<ApplicationUserDto>(user);
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
            UserCode = user.UserCode
        };

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

    public async Task<string> GetLastUserCode(CancellationToken cancellationToken)
    {
        return await _userManager.Users.MaxAsync(u => u.UserCode, cancellationToken);
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

    public async Task<Result> UpdateUserPinAsync(string userId, string newPIN)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure(new[] { "User not found." });

        user.UpdatePIN(newPIN);
        var result = await _userManager.UpdateAsync(user);
        return result.ToApplicationResult();
    }

    public async Task<Result> ActivateUserAsync(string userId, string newPassword, string userPin)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure(new[] { "User not found." });
        await _userManager.ChangePasswordAsync(user, "TempPwd123", newPassword);
        user.Activate();
        user.UpdatePIN(userPin);
        var result = await _userManager.UpdateAsync(user);
        return result.ToApplicationResult();
    }

    public async Task<Result> DeactivateUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure(new[] { "User not found." });
        user.Deactivate();
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

    public async Task<IList<UserRoleDto>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return [];

        var roles = await _userManager.GetRolesAsync(user);

        List<UserRoleDto> userRoles = [];
        foreach(var role in roles)
        {
            userRoles.Add(new UserRoleDto
            {
                UserId = userId,
                RoleId = role
            });
        }
        return userRoles;
    }

    public async Task<bool> RoleExistAsync(string roleName)
    {
        return await _roleManager.Roles.AnyAsync(v => v.Name == roleName);
    }

    public async Task<IList<IdentityRoleDto>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        IQueryable<IdentityRole> roles = _roleManager.Roles.AsNoTracking().Where(r => r.Name != Domain.Constants.Roles.Administrator);

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

        IQueryable<IdentityRole> roles = _roleManager.Roles.Where(r => r.Name != Domain.Constants.Roles.Administrator)
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

        const int expiresInMinutes = 60;

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserCode.Equals(userCode));

        if (user is null)
        {
            return null;
        }

        bool isAdmin = await _userManager.IsInRoleAsync(user, Domain.Constants.Roles.Administrator);

        if (user.IsActive == false && !isAdmin)
        {
            var passwordResult = await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure: true);

            if (!passwordResult.Succeeded)
            {
                return null;
            }

            return new TokenResponse
            {
                IsActive = user.IsActive,
                TokenType = null,
                AccessToken = "",
                ExpiresIn = 0,
                RefreshToken = ""
            }; 
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


        return new TokenResponse
        {
            IsActive = user.IsActive,
            TokenType = null,
            AccessToken = _tokenFactory.GenerateAccessToken(user, expiresInMinutes),
            ExpiresIn = expiresInMinutes * 60,
            RefreshToken = _tokenFactory.GenerateRefreshToken()
        };
    }

    public async Task<TokenResponse?> ActivateAsync(string userCode, string password, string pin)
    {
        if (string.IsNullOrEmpty(pin) ||
            pin.Length != 6 ||
            pin.Any(character => character < '0' || character > '9'))
        {
            return null;
        }

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserCode.Equals(userCode));
        if (user is null || user.IsActive || !string.IsNullOrEmpty(user.UserPin))
        {
            return null;
        }

        var passwordResult = await _signInManager.CheckPasswordSignInAsync(
            user,
            password,
            lockoutOnFailure: true);

        if (!passwordResult.Succeeded)
        {
            return null;
        }

        user.UpdatePIN(pin);
        user.Activate();
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var errors = string.Join("; ", updateResult.Errors.Select(error => error.Description));
            throw new InvalidOperationException($"Unable to activate the user: {errors}");
        }

        const int expiresInMinutes = 60;

        return new TokenResponse
        {
            IsActive = user.IsActive,
            TokenType = null,
            AccessToken = _tokenFactory.GenerateAccessToken(user, expiresInMinutes),
            ExpiresIn = expiresInMinutes * 60,
            RefreshToken = _tokenFactory.GenerateRefreshToken()
        };
    }
#endregion
}
