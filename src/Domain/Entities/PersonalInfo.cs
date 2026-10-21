namespace Delex_POS.Domain.Entities;

public class PersonalInfo : BaseAuditableEntity
{
    public string? Title { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public string? NameExt { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public CivilStatus CivilStatus { get; private set; }
    public string? Email { get; private set; }
    public string? BillingAddress1 { get; private set; }
    public string? BillingAddress2 { get; private set; }
    public string RegionCode { get; private set; }
    public string ProvinceCode { get; private set; }
    public string MunicipalityCode { get; private set; }
    public string BarangayCode { get; private set; }
    public string? Phone { get; private set; }
    public string? Mobile { get; private set; }
    public string? Fax { get; private set; }
    public string? Tin { get; private set; }
    public string? ContactPerson { get; private set; }
    public PersonalInfo(string? title, string? firstName, string? lastName, string? middleName,
        string? nameExt, DateOnly birthDate, Gender gender, CivilStatus civilStatus, 
        string? email, string? billingAddress1, string? billingAddress2, string regionCode, 
        string provinceCode, string municipalityCode, string barangayCode, 
        string? phone, string? mobile, string? fax, string? tin, string? contactPerson)
    {
        Title = title ?? string.Empty;
        FirstName = firstName ?? string.Empty;
        LastName = lastName ?? string.Empty;
        MiddleName = middleName ?? string.Empty;
        NameExt = nameExt ?? string.Empty;
        BirthDate = birthDate;
        Gender = gender;
        CivilStatus = civilStatus;
        Email = email ?? string.Empty;
        BillingAddress1 = billingAddress1 ?? string.Empty;
        BillingAddress2 = billingAddress2 ?? string.Empty;
        RegionCode = regionCode;
        ProvinceCode = provinceCode;
        MunicipalityCode = municipalityCode;
        BarangayCode = barangayCode;
        Phone = phone ?? string.Empty;
        Mobile = mobile ?? string.Empty;
        Fax = fax ?? string.Empty;
        Tin = tin ?? string.Empty;
        ContactPerson = contactPerson ?? string.Empty;
    }

    public void Update(string? title, string? firstName, string? lastName, string? middleName,
        string? nameExt, DateOnly birthDate, Gender gender, CivilStatus civilStatus, 
        string? email, string? billingAddress1, string? billingAddress2, string regionCode, 
        string provinceCode, string municipalityCode, string barangayCode, 
        string? phone, string? mobile, string? fax, string? tin, string? contactPerson)
    {
        Title = title ?? string.Empty;
        FirstName = firstName ?? string.Empty;
        LastName = lastName ?? string.Empty;
        MiddleName = middleName ?? string.Empty;
        NameExt = nameExt ?? string.Empty;
        BirthDate = birthDate;
        Gender = gender;
        CivilStatus = civilStatus;
        Email = email ?? string.Empty;
        BillingAddress1 = billingAddress1 ?? string.Empty;
        BillingAddress2 = billingAddress2 ?? string.Empty;
        RegionCode = regionCode;
        ProvinceCode = provinceCode;
        MunicipalityCode = municipalityCode;
        BarangayCode = barangayCode;
        Phone = phone ?? string.Empty;
        Mobile = mobile ?? string.Empty;
        Fax = fax ?? string.Empty;
        Tin = tin ?? string.Empty;
        ContactPerson = contactPerson ?? string.Empty;
    }
}
