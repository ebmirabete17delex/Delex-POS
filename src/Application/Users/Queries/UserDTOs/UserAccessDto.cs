namespace Delex_POS.Application.Users.Queries.UserDTOs;

public class UserAccessDto
{
    public int Id { get; init; } 
    public string UserId { get; init; } = string.Empty;
    public int AccessId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Feature { get; init; } = string.Empty;
    public string BackendUrl { get; init; } = string.Empty;
    public string FrontendUrl { get; init; } = string.Empty;
}