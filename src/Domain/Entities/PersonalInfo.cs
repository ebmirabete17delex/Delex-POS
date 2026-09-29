using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class PersonalInfo : BaseAuditableEntity
{
    public int AcctId { get; init; }
    public string? Title { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public string? NameExt { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string? Gender { get; private set; }
    public string? CivilStatus { get; private set; }
    public string? Nationality { get; private set; }
    public string? Email { get; private set; }
    public string? BillingAddress1 { get; private set; }
    public string? Phone { get; private set; }
    public string? Mobile { get; private set; }
    public string? Fax { get; private set; }
    public string? TIN { get; private set; }
    public PersonalInfo(int acctId, string? title, string? firstName, string? lastName, string? middleName,
        string? nameExt, DateOnly birthDate, string? gender, string? civilStatus, string? nationality,
        string? email, string? billingAddress1, string? phone, string? mobile, string? fax, string? tin)
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
        Nationality = nationality ?? string.Empty;
        Email = email ?? string.Empty;
        BillingAddress1 = billingAddress1 ?? string.Empty;
        Phone = phone ?? string.Empty;
        Mobile = mobile ?? string.Empty;
        Fax = fax ?? string.Empty;
        TIN = tin ?? string.Empty;
    }

    public void Update(string? title, string? firstName, string? lastName, string? middleName,
        string? nameExt, DateOnly birthDate, string? gender, string? civilStatus, string? nationality,
        string? email, string? billingAddress1, string? phone, string? mobile, string? fax, string? tin)
    {
        Title = title ?? string.Empty;
        FirstName = firstName ?? string.Empty;
        LastName = lastName ?? string.Empty;
        MiddleName = middleName ?? string.Empty;
        NameExt = nameExt ?? string.Empty;
        BirthDate = birthDate;
        Gender = gender ?? string.Empty;
        CivilStatus = civilStatus ?? string.Empty;
        Nationality = nationality ?? string.Empty;
        Email = email ?? string.Empty;
        BillingAddress1 = billingAddress1 ?? string.Empty;
        Phone = phone ?? string.Empty;
        Mobile = mobile ?? string.Empty;
        Fax = fax ?? string.Empty;
        TIN = tin ?? string.Empty;

    }
}
