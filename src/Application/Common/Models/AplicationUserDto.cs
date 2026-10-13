namespace Delex_POS.Application.Common.Models;

public class ApplicationUserDto
{
    public string UserCode { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

    public ApplicationUserDto(string userCode, 
                            string email)
    {
        UserCode = userCode;
        Email = email;
    }
}
