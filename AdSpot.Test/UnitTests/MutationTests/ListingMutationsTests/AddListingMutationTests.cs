using AdSpot.Models;

namespace AdSpot.Test.UnitTests.ListingMutationsTests;

[Collection("adspot-inmemory-db")]
public class AddListingMutationTests
{
    private const string AddListingMutation = """
        mutation AddListing($input: AddListingInput!) {
            addListing(input: $input) {
                listing {
                    listingId
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
    public async Task AddListingSuccessful()
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
                b.SetQuery(AddListingMutation)
                    .SetVariableValue(
                        "input",
                        new Dictionary<string, object?>
                        {
                            { "listingTypeId", 1 },
                            { "userId", TestDatabase.TestUser.UserId },
                            { "price", (decimal)9.99 }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddListingInvalidListingTypeId()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(AddListingMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "listingTypeId", -1 },
                        { "userId", TestDatabase.TestUser.UserId },
                        { "price", (decimal)9.99 }
                    }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddListingAccountHasNotBeenConnected()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(AddListingMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "listingTypeId", TestDatabase.ListingTypes.Last().ListingTypeId },
                        { "userId", TestDatabase.TestUser.UserId },
                        { "price", (decimal)9.99 }
                    }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }
}
