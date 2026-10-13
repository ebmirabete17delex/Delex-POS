using Microsoft.AspNetCore.Identity;
namespace Delex_POS.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string UserCode { get; init; } = string.Empty;
    public string UserPin { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public void UpdatePIN(string newPin)
    {
        UserPin = newPin;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
}
