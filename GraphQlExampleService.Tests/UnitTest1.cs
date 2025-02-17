using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQlExampleService.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        // Arrange
        var query = @"mutation addAuthor {
                          addAuthor(input: { name: ""Schiller""}) {
                            record {
                              id
                              name
                            }
                          }
                        }";

        var graphQlServer = new ServiceCollection()
            .AddSingleton<Repository>()
            .AddGraphQLServer()
            .AddQueryType(q => q.Name("Query"))
            .AddType<FirstQuery>()
            .AddType<SecondQuery>()
            .AddMutationType(m => m.Name("Mutation"))
            .AddType<FirstMutation>()
            .AddType<SecondMutation>();

        // Act
        var response = await graphQlServer.ExecuteRequestAsync(query);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response as OperationResult);
        Assert.Null((response as OperationResult).Errors);
    }
}
