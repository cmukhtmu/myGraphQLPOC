using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLPeopleApi.Models;

[Table("AddressHistory")]
public class AddressHistory
{
    [Column("ID")]
    public long Id { get; set; }

    [Column("Address1")]
    public string? Address1 { get; set; }

    [Column("Address2")]
    public string? Address2 { get; set; }

    [Column("City")]
    public string? City { get; set; }

    [Column("State")]
    public string? State { get; set; }

    [Column("Zip")]
    public string? Zip { get; set; }

    [Column("Active")]
    public bool? Active { get; set; }

    [Column("PeopleID")]
    public long PeopleId { get; set; }

    public Person? Person { get; set; }
}
