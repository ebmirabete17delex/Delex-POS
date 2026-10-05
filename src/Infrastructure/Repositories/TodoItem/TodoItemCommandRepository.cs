using Delex_POS.Application.Common.Interfaces.Repositories.TodoItem;
using Delex_POS.Infrastructure.Data;

namespace Delex_POS.Infrastructure.Repositories.TodoItem;

public class TodoItemCommandRepository : CommandHandlerBase<Domain.Entities.TodoItem>, ITodoItemCommandRepository
{
    public TodoItemCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
