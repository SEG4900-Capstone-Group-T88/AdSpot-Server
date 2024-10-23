namespace AdSpot.Api.Queries;

[QueryType]
public class PlatformQueries
{
    [UseProjection]
    [UseSorting]
    public IQueryable<Platform> GetPlatforms(PlatformRepository repo)
    {
        return repo.GetAllPlatforms();
    }
}
