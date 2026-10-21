namespace Delex_POS.Application.Common.Interfaces.Repositories.RefreshToken;

public interface IRefreshTokenCommandRepository : ICommandHandlerBase<Domain.Entities.RefreshToken>
{
    Task RemoveTokenAsync(int id, CancellationToken cancellationToken);
}
