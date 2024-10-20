using AdSpot.Models;

namespace AdSpot.Test.UnitTests.OrderMutationsTests;

[Collection("adspot-inmemory-db")]
public class OrderListingMutationsTests
{
    public const string OrderListingMutation = """
            mutation OrderListing($input: OrderListingInput!) {
                orderListing(input: $input) {
                    order {
                        listingId
                        userId
                        orderId
                        description
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
    public async Task OrderListingSuccessful()
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

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId,
                        Price = 100.00M
                    }
                );
                context.SaveChanges();

                context.Users.Add(
                    new User
                    {
                        UserId = 2,
                        Email = "testuser2@adspot.com",
                        Password = "testuserpassword2",
                        FirstName = "Test",
                        LastName = "User 2"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetQuery(OrderListingMutation)
                    .SetVariableValue(
                        "input",
                        new Dictionary<string, object?>
                        {
                            { "description", "Test Order" },
                            { "listingId", 1 },
                            { "price", 100.00M },
                            { "userId", 2 }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task OrderListingInvalidIdError()
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

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId,
                        Price = 100.00M
                    }
                );
                context.SaveChanges();

                context.Users.Add(
                    new User
                    {
                        UserId = 2,
                        Email = "testuser2@adspot.com",
                        Password = "testuserpassword2",
                        FirstName = "Test",
                        LastName = "User 2"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetQuery(OrderListingMutation)
                    .SetVariableValue(
                        "input",
                        new Dictionary<string, object?>
                        {
                            { "description", "Test Order" },
                            { "listingId", -1 },
                            { "price", 100.00M },
                            { "userId", 2 }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task OrderListingOwnListingError()
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

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId,
                        Price = 100.00M
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetQuery(OrderListingMutation)
                    .SetVariableValue(
                        "input",
                        new Dictionary<string, object?>
                        {
                            { "description", "Test Order" },
                            { "listingId", 1 },
                            { "price", 100.00M },
                            { "userId", TestDatabase.TestUser.UserId }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task OrderListingPriceChangedError()
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

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId,
                        Price = 100.00M
                    }
                );
                context.SaveChanges();

                context.Users.Add(
                    new User
                    {
                        UserId = 2,
                        Email = "testuser2@adspot.com",
                        Password = "testuserpassword2",
                        FirstName = "Test",
                        LastName = "User 2"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetQuery(OrderListingMutation)
                    .SetVariableValue(
                        "input",
                        new Dictionary<string, object?>
                        {
                            { "description", "Test Order" },
                            { "listingId", 1 },
                            { "price", 120.00M },
                            { "userId", 2 }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }
}
