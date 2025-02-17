var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<Repository>()
    // .AddAuthentication().Services // Configure your Authentication here
    // .AddAuthorization(o => o.AddPolicy("Librarian", p => p.RequireAssertion(_ => false)))
    .AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddType<FirstQuery>()
    .AddType<SecondQuery>()
    .AddMutationType(m => m.Name("Mutation"))
    .AddType<FirstMutation>()
    .AddType<SecondMutation>();
    // .AddAuthorization();
var app = builder.Build();

app.MapGraphQL();

app.Run();