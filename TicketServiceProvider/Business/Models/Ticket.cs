using System.ComponentModel.DataAnnotations.Schema;
using TicketServiceProvider.Data.Entities;

namespace TicketServiceProvider.Business.Models;

public class Ticket
{
    public string Id { get; set; } = null!;

    public string EventId { get; set; } = null!;
    public string EventName { get; set; } = null!;

    public int TicketAmount { get; set; }

    public double TicketPrice { get; set; }

    public Category Category { get; set; } = null!;
}
