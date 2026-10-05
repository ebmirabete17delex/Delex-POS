namespace Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
public interface IPersonalInfoQueryRepository : IQueryHandlerBase<Domain.Entities.PersonalInfo>
{
    Task<Domain.Entities.PersonalInfo?> GetByAccountIdAsync(int id, CancellationToken cancellationToken);
}
