namespace AdSpot.Test.UnitTests.UserMutationsTests;

[Collection("adspot-inmemory-db")]
public class AddUserMutationTests
{
    private const string AddUserMutation = """
        mutation AddUser($input: AddUserInput!) {
            addUser(input: $input) {
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
    public async Task AddUserSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(AddUserMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "email", "newadspotuser@adspot.com" },
                        { "password", "newadspotuser" },
                        { "firstName", "new" },
                        { "lastName", "user" },
                    }
                )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddUserEmailAlreadyExists()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(AddUserMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "email", TestDatabase.TestUser.Email },
                        { "password", TestDatabase.TestUser.Password },
                        { "firstName", TestDatabase.TestUser.FirstName },
                        { "lastName", TestDatabase.TestUser.LastName },
                    }
                )
        );

        result.MatchSnapshot();
    }
}
