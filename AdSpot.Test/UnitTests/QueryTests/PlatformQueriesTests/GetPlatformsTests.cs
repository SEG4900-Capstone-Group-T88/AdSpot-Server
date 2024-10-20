using AdSpot.Models;

namespace AdSpot.Test.UnitTests.PlatformQueriesTests;

[Collection("adspot-inmemory-db")]
public class GetPlatformsTests
{
    public const string GetAllPlatformsQuery = """
        query GetPlatforms{
            platforms {
                platformId
                name
            }
        }
        """;

    //[Fact]
    [Trait("Category", "Unit")]
    public async Task GetPlatformsSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(b => b.SetQuery(GetAllPlatformsQuery));

        result.MatchSnapshot();
    }
}
