using GraphQLPeopleApi.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
public class PersonType : ObjectType<Person>
{
    protected override void Configure(IObjectTypeDescriptor<Person> descriptor)
    {
        descriptor.Field(p => p.AddressHistory)
            .UseFiltering()
            .UseSorting();            
    }
}
