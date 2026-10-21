using Delex_POS.Domain.Entities;
namespace Delex_POS.Application.Employees.Queries.EmployeeDTOs;

public class UserAccessDto
{
    public int Id { get; init; } 
    public string UserId { get; init; } = string.Empty;
    public int AccessId { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserAccess, UserAccessDto>();
        }
    }
}
