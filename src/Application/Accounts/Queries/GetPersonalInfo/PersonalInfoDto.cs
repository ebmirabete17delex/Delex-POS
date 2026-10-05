using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Accounts.Queries.GetPersonalInfo;

public class PersonalInfoDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? NameExt { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? CivilStatus { get; set; }
    public string? Nationality { get; set; }
    public string? Email { get; set; }
    public string? BillingAddress1 { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Fax { get; set; }
    public string? TIN { get; set; }
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<PersonalInfo, PersonalInfoDto>();
        }
    }
}
