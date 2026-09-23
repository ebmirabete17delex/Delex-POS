using AutoMapper;
using Delex_POS.Application.Common.Models;
using Delex_POS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Delex_POS.Infrastructure.Mapping.IdentityProfile;
public class IdentityProfile : Profile
{
	public IdentityProfile()
	{
		CreateMap<ApplicationUser, ApplicationUserDto>();
        CreateMap<IdentityRole, IdentityRoleDto>();
		// Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
	}
}