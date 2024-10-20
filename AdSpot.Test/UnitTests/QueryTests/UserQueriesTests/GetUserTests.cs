using AdSpot.Models;

namespace AdSpot.Test.UnitTests.UserQueriesTests;

[Collection("adspot-inmemory-db")]
public class GetUserTests
{
    public const string GetUserQuery = """
        query GetUser ($userId:Int!) {
            userById (userId: $userId) {
                userId
                firstName
                lastName
                email
            }
        }
        """;

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetUserSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(GetUserQuery).SetVariableValue("userId", TestDatabase.TestUser.UserId)
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetUserNotFound()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(GetUserQuery).SetVariableValue("userId", -1)
        );

        result.MatchSnapshot();
    }
}
