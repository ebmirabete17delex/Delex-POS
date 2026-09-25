//using System;
//using System.Collections.Generic;
//using System.Text;
//using Delex_POS.Application.Common.Interfaces;
//using Microsoft.Extensions.Logging;

//namespace Delex_POS.Application.Common.Behaviours;

//public interface ITransactional;

//public class TransactionBehavior<TRequest, TResponse>
//    : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>, ITransactional
//{
//    private readonly IApplicationDbContext _dbContext;
//    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

//    public TransactionBehavior(
//        IApplicationDbContext dbContext,
//        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
//    {
//        _dbContext = dbContext;
//        _logger = logger;
//    }

//    public async Task<TResponse> Handle(
//        TRequest request,
//        RequestHandlerDelegate<TResponse> next,
//        CancellationToken cancellationToken)
//    {
//        await using var transaction = await _dbContext.Database
//            .BeginTransactionAsync(cancellationToken);

//        try
//        {
//            var response = await next();

//            await _dbContext.SaveChangesAsync(cancellationToken);
//            await transaction.CommitAsync(cancellationToken);

//            _logger.LogInformation(
//                "Transaction committed for {Request}",
//                typeof(TRequest).Name);

//            return response;
//        }
//        catch
//        {
//            await transaction.RollbackAsync(cancellationToken);
//            throw;
//        }
//    }
//}
