using System.ComponentModel.DataAnnotations;

namespace TicketServiceProvider.Data.Entities;

public class CategoryEntity
{
    [Key]
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;

    public ICollection<TicketEntity> Tickets { get; set; } = [];
}
