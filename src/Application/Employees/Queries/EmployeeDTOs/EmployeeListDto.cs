using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.Employees.Queries.EmployeeDTOs;

public class EmployeeListDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime Created { get; set; }
}
