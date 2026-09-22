using Delex_POS.Domain.Entities.RBAC;
namespace Delex_POS.Application.Users.Queries.UserDTOs;
public class UserDto
{
    public int Id { get; init; }
    public int BranchId { get; init; }
    public Branch? Branch { get; init; } 
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTimeOffset AddedOn { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
};