using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLPeopleApi.Models;

[Table("People")]
public class Person
{
    [Column("ID")]
    public long Id { get; set; }

    [Column("First_Name")]
    public string? FirstName { get; set; }

    [Column("Last_Name")]
    public string? LastName { get; set; }

    [Column("Phone")]
    public string? Phone { get; set; }

    public ICollection<AddressHistory> AddressHistory { get; set; } = new List<AddressHistory>();
}
