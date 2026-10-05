using Delex_POS.Application.Common.Interfaces.Repositories.TodoItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TodoItem;

public class TodoItemQueryRepository : QueryHandlerBase<Domain.Entities.TodoItem>, ITodoItemQueryRepository
{
    public TodoItemQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
