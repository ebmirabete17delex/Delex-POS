using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Accounts.Queries.AccountDTOs;
public class AccountDto
{
    public int Id { get; init; }
    public int AccountId { get; init; }
    public string Name { get; set; } = string.Empty;
    public string AltKey { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int BranchId { get; set; }
    public bool IsRestrictToAssignedBranchOnly { get; set; }
    public bool IsAllowToLogon { get; set; }
    public string? Password { get; set; }
    public string AccessLevel { get; set; } = string.Empty;
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Account, AccountDto>();
        }
    }
};
