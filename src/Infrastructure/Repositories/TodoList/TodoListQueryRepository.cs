using Delex_POS.Application.Common.Interfaces.Repositories.TodoList;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TodoList;

public class TodoListQueryRepository : QueryHandlerBase<Domain.Entities.TodoList>, ITodoListQueryRepository
{
    public TodoListQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
