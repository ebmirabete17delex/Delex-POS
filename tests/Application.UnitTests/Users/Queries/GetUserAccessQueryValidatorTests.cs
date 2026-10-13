using Delex_POS.Application.Employees.Queries.GetUserAccess;
using NUnit.Framework;

namespace Delex_POS.Application.UnitTests.Users.Queries;

public class GetUserAccessQueryValidatorTests
{
    [Test]
    public async Task ShouldRejectMissingUserId()
    {
        var query = new GetUserAccessQuery();
        var result = await new GetUserAccessQueryValidator().ValidateAsync(query);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(error => error.PropertyName == nameof(GetUserAccessQuery.Id)), Is.True);
    }

    [Test]
    public async Task ShouldAllowOptionalSearchAndSortValues()
    {
        var query = new GetUserAccessQuery()
        {
            Id = 1
        };
        var result = await new GetUserAccessQueryValidator().ValidateAsync(query);

        Assert.That(result.IsValid, Is.True);
    }
}
