namespace AdSpot.Test.UnitTests.UserMutationsTests;

[Collection("adspot-inmemory-db")]
public class LoginMutationTests
{
    private const string LoginMutation = """
        mutation LoginMutation($input: LoginInput!) {
            login(input: $input) {
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
    public async Task LoginSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(LoginMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "email", TestDatabase.TestUser.Email },
                        { "password", TestDatabase.TestUser.Password },
                    }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task LoginInvalidCredentials()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(LoginMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "email", TestDatabase.TestUser.Email },
                        { "password", TestDatabase.TestUser.Password + "invalid" },
                    }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task LoginUserDoesNotExist()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(LoginMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "email", "userdoesnotexist" },
                        { "password", "userdoesnotexist" },
                    }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }
}
