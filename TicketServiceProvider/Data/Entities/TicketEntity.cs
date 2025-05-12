using System.ComponentModel.DataAnnotations;

namespace TicketServiceProvider.Data.Entities;

public class TicketEntity
{
    [Key]
    public string EventId { get; set; } = null!;
    public int TicketAmount { get; set; }
    public int TicketPrice { get; set; }
}
