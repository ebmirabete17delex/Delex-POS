using Delex_POS.Application.Common.Interfaces.Repositories.TodoList;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TodoList;

public class TodoListCommandRepository : CommandHandlerBase<Domain.Entities.TodoList>, ITodoListCommandRepository
{
    public TodoListCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
