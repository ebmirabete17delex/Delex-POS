using Microsoft.AspNetCore.Identity;

namespace Delex_POS.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string UserCode { get; set; } = string.Empty;
}
