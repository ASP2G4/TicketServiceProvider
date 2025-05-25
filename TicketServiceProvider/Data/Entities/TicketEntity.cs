using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketServiceProvider.Data.Entities;

public class TicketEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string EventId { get; set; } = null!;
    public string EventName { get; set; } = null!;

    public int TicketAmount { get; set; }


    public int TicketPrice { get; set; }

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
}
