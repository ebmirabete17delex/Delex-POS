namespace Delex_POS.Domain.Entities;

public class PersonalInfo : BaseAuditableEntity
{
    public int AcctId { get; init; }
    public string? Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? NameExt { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Gender { get; set; }
    public string CivilStatus { get; set; }
    public int NationalityId { get; set; }
    public string? Email { get; set; }
    public string? BillingAddress1 { get; set; }
    public string? BillingAddress2 { get; set; }
    public string? BillingAddress3 { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Fax { get; set; }
    public string? Tin { get; set; }
    public string? ContactPerson { get; set; }
    public PersonalInfo(int acctId, string? title, string? firstName, string? lastName, string? middleName,
        string? nameExt, DateOnly birthDate, string? gender, string? civilStatus, int nationalityId,
        string? email, string? billingAddress1, string? billingAddress2, string? billingAddress3, 
        string? phone, string? mobile, string? fax, string? tin, string? contactPerson)
    {
        AcctId = acctId;
        Title = title ?? string.Empty;
        FirstName = firstName ?? string.Empty;
        LastName = lastName ?? string.Empty;
        MiddleName = middleName ?? string.Empty;
        NameExt = nameExt ?? string.Empty;
        BirthDate = birthDate;
        Gender = gender ?? string.Empty;
        CivilStatus = civilStatus ?? string.Empty;
        NationalityId = nationalityId;
        Email = email ?? string.Empty;
        BillingAddress1 = billingAddress1 ?? string.Empty;
        BillingAddress2 = billingAddress2 ?? string.Empty;
        BillingAddress3 = billingAddress3 ?? string.Empty;
        Phone = phone ?? string.Empty;
        Mobile = mobile ?? string.Empty;
        Fax = fax ?? string.Empty;
        Tin = tin ?? string.Empty;
        ContactPerson = contactPerson ?? string.Empty;
    }

    public void Update(string? title, string? firstName, string? lastName, string? middleName,
        string? nameExt, DateOnly birthDate, string? gender, string? civilStatus, int nationalityId,
        string? email, string? billingAddress1, string? billingAddress2, string? billingAddress3, 
        string? phone, string? mobile, string? fax, string? tin, string? contactPerson)
    {
        Title = title ?? string.Empty;
        FirstName = firstName ?? string.Empty;
        LastName = lastName ?? string.Empty;
        MiddleName = middleName ?? string.Empty;
        NameExt = nameExt ?? string.Empty;
        BirthDate = birthDate;
        Gender = gender ?? string.Empty;
        CivilStatus = civilStatus ?? string.Empty;
        NationalityId = nationalityId;
        Email = email ?? string.Empty;
        BillingAddress1 = billingAddress1 ?? string.Empty;
        BillingAddress2 = billingAddress2 ?? string.Empty;
        BillingAddress3 = billingAddress3 ?? string.Empty;
        Phone = phone ?? string.Empty;
        Mobile = mobile ?? string.Empty;
        Fax = fax ?? string.Empty;
        Tin = tin ?? string.Empty;
        ContactPerson = contactPerson ?? string.Empty;
    }
}
