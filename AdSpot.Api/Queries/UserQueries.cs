﻿namespace AdSpot.Api.Queries;

[QueryType]
public class UserQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers(UserRepository repo)
    {
        return repo.GetAllUsers();
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<User> GetUserById(int userId, UserRepository repo)
    {
        return repo.GetUserById(userId);
    }
}
