using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SmartBazaar.API.Models;


[Table("markets")] // This must match the table name in Supabase
public class Market : BaseModel
{
    [PrimaryKey("id", false)] // false means Supabase generates the ID
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("owner_id")]
    public Guid OwnerId { get; set; }

    [Column("location")]
    public string Location { get; set; } = string.Empty;
}