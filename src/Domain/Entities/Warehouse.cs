using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class Warehouse : BaseAuditableEntity
{
    public string Name { get; init; } = string.Empty;
    public string? Memo {  get; private set; }
    public bool IsActive { get; private set; }
    public string? Address1 { get; private set; }
    public string? Address2 { get; private set; }
    public string? Address3 { get; private set; }
    public string? Phone { get; private set; }
    public string? Fax { get; private set; }
    public string? Email { get; private set; }
    public Warehouse (string name, string? memo, string? address1, string? address2, 
        string? address3,  string? phone, string? fax, string email)
    {
        Name = name;
        Memo = memo;
        Address1 = address1;
        Address2 = address2;
        Address3 = address3;
        Phone = phone;
        Fax = fax;
        Email = email;
    }
    public void Update(string? memo, string? address1, string? address2,
        string? address3, string? phone, string? fax, string email)
    {
        Memo = memo;
        Address1 = address1;
        Address2 = address2;
        Address3 = address3;
        Phone = phone;
        Fax = fax;
        Email = email;
    }
}
