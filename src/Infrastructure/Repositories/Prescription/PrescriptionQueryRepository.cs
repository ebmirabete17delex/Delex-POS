using Delex_POS.Application.Common.Interfaces.Repositories.Prescription;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Prescription;

public class PrescriptionQueryRepository : QueryHandlerBase<Domain.Entities.Prescription>, IPrescriptionQueryRepository
{
    public PrescriptionQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
