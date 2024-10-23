using AdSpot.Models;

namespace AdSpot.Test.UnitTests.UserMutationsTests;

[Collection("adspot-inmemory-db")]
public class UpdatePasswordMutationTests
{
    private const string UpdatePasswordMutation = """
            mutation UpdatePassword($input: UpdatePasswordInput!) {
                updatePassword(input: $input) {
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
    public async Task UpdatePasswordSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(UpdatePasswordMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "userId", TestDatabase.TestUser.UserId },
                        { "password", "UpdatedTestPassword" }
                    }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task UpdatePasswordInvalidUserIdError()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(UpdatePasswordMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "userId", 2 },
                        { "password", "UpdatedTestPassword" }
                    }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }
}
