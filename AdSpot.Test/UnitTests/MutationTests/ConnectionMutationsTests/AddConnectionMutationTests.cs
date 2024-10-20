using AdSpot.Models;

namespace AdSpot.Test.UnitTests.ConnectionMutationsTests;

[Collection("adspot-inmemory-db")]
public class AddConnectionMutationTests
{
    public const string AddConnectionMutation = """
        mutation AddConnection($input: AddConnectionInput!) {
            addConnection(input: $input) {
                connection {
                    userId
                    platformId
                }
                errors {
                    ... on Error {
                        __typename
                        message
                    }
                }
            }
        }
        """;

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddConnectionSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(AddConnectionMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "userId", TestDatabase.TestUser.UserId },
                        { "platformId", TestDatabase.Platforms.First().PlatformId },
                        { "accountHandle", "TestAccountHandle" },
                        { "apiToken", "TestApiToken" }
                    }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddConnectionTwice()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetQuery(AddConnectionMutation)
                    .SetVariableValue(
                        "input",
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "platformId", TestDatabase.Platforms.First().PlatformId },
                            { "accountHandle", "TestAccountHandle" },
                            { "apiToken", "TestApiToken" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }
}
