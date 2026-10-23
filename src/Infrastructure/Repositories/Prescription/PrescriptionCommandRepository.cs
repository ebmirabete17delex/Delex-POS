using Delex_POS.Application.Common.Interfaces.Repositories.Prescription;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.Prescription;

public class PrescriptionCommandRepository : CommandHandlerBase<Domain.Entities.Prescription>, IPrescriptionCommandRespository
{
    public PrescriptionCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
