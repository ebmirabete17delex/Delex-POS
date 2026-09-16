using Delex_POS.Application.Common.Exceptions;
using Delex_POS.Application.TodoLists.Commands.CreateTodoList;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.FunctionalTests.TodoLists.Commands;

public class CreateTodoListTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoListCommand();
        await Should.ThrowAsync<ValidationException>(() => TestApp.SendAsync(command));
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await TestApp.SendAsync(new CreateTodoListCommand
        {
            Title = "Shopping"
        });

        var command = new CreateTodoListCommand
        {
            Title = "Shopping"
        };

        await Should.ThrowAsync<ValidationException>(() => TestApp.SendAsync(command));
    }

    [Test]
    public async Task ShouldCreateTodoList()
    {
        var userId = await TestApp.RunAsDefaultUserAsync();

        var command = new CreateTodoListCommand
        {
            Title = "Tasks"
        };

        var id = await TestApp.SendAsync(command);

        var list = await TestApp.FindAsync<TodoList>(id);

        list.ShouldNotBeNull();
        list!.Title.ShouldBe(command.Title);
        list.CreatedBy.ShouldBe(userId);
        list.Created.ShouldBe(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
