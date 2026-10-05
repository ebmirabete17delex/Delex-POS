using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class Generic : BaseAuditableEntity
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Memo {  get; private set; } = string.Empty;
    public Generic (string code, string name, string? memo)
    {
        Code = code;
        Name = name;
        Memo = memo;
    }

    public void Update(string name, string? memo)
    {
        Name = name;
        Memo = memo ?? string.Empty;
    }
}
