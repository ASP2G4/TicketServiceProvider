using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketServiceProvider.Data.Contexts;
using TicketServiceProvider.Data.Entities;

namespace TicketServiceProvider.Data.Repositories;

public interface ITicketRepository
{
    Task<bool> DeleteAsync(Expression<Func<TicketEntity, bool>> expression);
    Task<IEnumerable<TicketEntity>> GetAllAsync();
    Task<IEnumerable<TicketEntity>?> GetAsync(string eventId);
    Task<bool> UpdateAsync(TicketEntity entity);
}

public class TicketRepository : ITicketRepository
{
    protected readonly DataContext _context;
    protected readonly DbSet<TicketEntity> _table;

    public TicketRepository(DataContext context)
    {
        _context = context;
        _table = context.Set<TicketEntity>();
    }


    /* Read */
    public virtual async Task<IEnumerable<TicketEntity>> GetAllAsync()
    {
        var entities = await _table.Include(t => t.Category).ToListAsync();
        return entities;
    }

    public virtual async Task<IEnumerable<TicketEntity>?> GetAsync(string eventId)
    {
        var entity = await _table.Include(t => t.Category).Where(t => t.EventId == eventId).ToListAsync();
        return entity;
    }


    /* Update */
    public virtual async Task<bool> UpdateAsync(TicketEntity entity)
    {
        if (entity == null)
            return false;

        _table.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }


    /* Delete */
    public virtual async Task<bool> DeleteAsync(Expression<Func<TicketEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        var entity = await _table.FirstOrDefaultAsync(expression);
        if (entity == null)
            return false;

        _table.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

}
