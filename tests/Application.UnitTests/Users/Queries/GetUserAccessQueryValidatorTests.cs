using Delex_POS.Application.Users.Queries.GetUserAccess;
using NUnit.Framework;

namespace Delex_POS.Application.UnitTests.Users.Queries;

public class GetUserAccessQueryValidatorTests
{
    [Test]
    public async Task ShouldRejectMissingUserId()
    {
        var query = new GetUserAccessQuery(1, 10, null, null, null);
        var result = await new GetUserAccessQueryValidator().ValidateAsync(query);

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(error => error.PropertyName == nameof(GetUserAccessQuery.UserId)), Is.True);
    }

    [Test]
    public async Task ShouldAllowOptionalSearchAndSortValues()
    {
        var query = new GetUserAccessQuery(1, 10, null, null, null)
        {
            UserId = "user-id"
        };
        var result = await new GetUserAccessQueryValidator().ValidateAsync(query);

        Assert.That(result.IsValid, Is.True);
    }
}
