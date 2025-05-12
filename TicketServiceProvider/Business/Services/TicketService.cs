using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TicketServiceProvider.Data.Contexts;
using TicketServiceProvider.Data.Entities;
using TicketServiceProvider.Data.Repositories;

namespace TicketServiceProvider.Business.Services;

public interface ITicketService
{
    Task<IEnumerable<TicketEntity>> GetAllTicketsAsync();
    Task<TicketEntity?> GetTicketByIdAsync(string eventId);
}

public class TicketService(DataContext context, ITicketRepository ticketRepository) : TicketContract.TicketContractBase, ITicketService
{
    private readonly DataContext _context = context;
    private readonly ITicketRepository _ticketRepository = ticketRepository;

    public override async Task<TicketReply> CreateTicket(TicketRequest request, ServerCallContext context)
    {
        try
        {
            var entity = new TicketEntity
            {
                EventId = request.EventId,
                TicketAmount = request.TicketAmount,
                TicketPrice = request.TicketPrice,
            };

            _context.Add(entity);
            await _context.SaveChangesAsync();

            return new TicketReply
            {
                Succeeded = true,
                Message = "New tickets were created"
            };
        }
        catch (Exception ex)
        {
            return new TicketReply
            {
                Succeeded = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<IEnumerable<TicketEntity>> GetAllTicketsAsync()
    {
        var entities = await _ticketRepository.GetAllAsync();
        var events = entities.Select(entity => new TicketEntity
        {
            EventId = entity.EventId,
            TicketAmount = entity.TicketAmount,
            TicketPrice = entity.TicketPrice,

        });

        return events;
    }

    public async Task<TicketEntity?> GetTicketByIdAsync(string eventId)
    {
        var entity = await _ticketRepository.GetAsync(x => x.EventId == eventId);
        return entity == null
            ? null
            : new TicketEntity
            {
                EventId = entity.EventId,
                TicketAmount = entity.TicketAmount,
                TicketPrice = entity.TicketPrice,
            };
    }
}
