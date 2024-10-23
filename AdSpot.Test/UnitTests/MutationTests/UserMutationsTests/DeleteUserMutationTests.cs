using AdSpot.Models;

namespace AdSpot.Test.UnitTests.UserMutationsTests;

[Collection("adspot-inmemory-db")]
public class DeleteUserMutationTests
{
    private const string DeleteUserMutation = """
            mutation DeleteUser($input: DeleteUserInput!) {
                deleteUser(input: $input) {
                    user {
                        userId
                        email
                        firstName
                        lastName
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
    public async Task DeleteUserSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
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
                b.SetQuery(DeleteUserMutation)
                    .SetVariableValue("input", new Dictionary<string, object?> { { "userId", 2 } }.AsReadOnly())
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task DeleteUserInvalidUserIdError()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(DeleteUserMutation)
                .SetVariableValue("input", new Dictionary<string, object?> { { "userId", 2 } }.AsReadOnly())
        );

        result.MatchSnapshot();
    }
}
