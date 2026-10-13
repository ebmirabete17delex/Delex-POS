using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.Employees.Queries.EmployeeDTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string? EmployeeId { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public int BranchId { get; set; }
    public int CustomerTypeId { get; set; }
    public string? Title { get; set; }
    public string? MiddleName { get; set; }
    public string? NameExt { get; set; }
    public string? Email { get; set; }
    public string? BillingAddress1 { get; set; }
    public string? BillingAddress2 { get; set; }
    public string? BillingAddress3 { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Fax { get; set; }
    public string? TIN { get; set; }
    public string? ContactPerson { get; set; }
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<PersonalInfo, EmployeeDto>();
        }
    }
}
