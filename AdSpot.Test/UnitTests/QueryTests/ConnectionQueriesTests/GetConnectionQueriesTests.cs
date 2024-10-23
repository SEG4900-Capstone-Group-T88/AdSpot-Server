using AdSpot.Models;

namespace AdSpot.Test.UnitTests.ConnectionQueriesTests;

[Collection("adspot-inmemory-db")]
public class GetConnectionQueriesTests
{
    public const string GetConnectionQuery = """
        query GetConnection($userId: Int!, $platformId: Int!) {
            connection(userId: $userId, platformId: $platformId) {
                userId
                platformId
            }
        }
        """;

    public const string GetConnectionsQuery = """
        query GetConnections($userId: Int!) {
            connections(userId: $userId) {
                userId
                platformId
            }
        }
        """;

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetConnectionSuccessful()
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
                b.SetQuery(GetConnectionQuery)
                    .SetVariableValue("userId", TestDatabase.TestUser.UserId)
                    .SetVariableValue("platformId", TestDatabase.Platforms.First().PlatformId)
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetConnectionEmpty()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(GetConnectionQuery)
                .SetVariableValue("userId", TestDatabase.TestUser.UserId)
                .SetVariableValue("platformId", TestDatabase.Platforms.First().PlatformId)
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetConnectionsSuccessful()
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
            b => b.SetQuery(GetConnectionsQuery).SetVariableValue("userId", TestDatabase.TestUser.UserId)
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetConnectionsEmpty()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(GetConnectionsQuery).SetVariableValue("userId", TestDatabase.TestUser.UserId)
        );

        result.MatchSnapshot();
    }
}
