using GraphQLPeopleApi.Data;
using GraphQLPeopleApi.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["ConnectionStrings__DefaultConnection"];

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<PersonType>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .ModifyCostOptions(options =>
    {
        options.MaxFieldCost = 50000;
        options.MaxTypeCost = 50000; 
        options.EnforceCostLimits = true;
    });
var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();
