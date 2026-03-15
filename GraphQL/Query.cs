using GraphQLPeopleApi.Data;
using GraphQLPeopleApi.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQLPeopleApi.GraphQL;

public class Query
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Person> GetPeople([Service] AppDbContext context)
        => context.People.Include(p => p.AddressHistory);

    [UseFiltering]
    [UseSorting]
    public IQueryable<AddressHistory> GetAddressHistory([Service] AppDbContext context)
        => context.AddressHistory;
}
