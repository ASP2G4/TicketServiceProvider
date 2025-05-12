using Microsoft.EntityFrameworkCore;
using TicketServiceProvider.Data.Entities;

namespace TicketServiceProvider.Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<TicketEntity> TicketEntities { get; set; }
}
