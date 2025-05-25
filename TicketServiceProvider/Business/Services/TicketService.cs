using Azure.Core;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TicketServiceProvider.Business.Models;
using TicketServiceProvider.Data.Contexts;
using TicketServiceProvider.Data.Entities;
using TicketServiceProvider.Data.Repositories;

namespace TicketServiceProvider.Business.Services;

public interface ITicketService
{
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<IEnumerable<Ticket>> GetTicketByEventIdAsync(string eventId);
}

public class TicketService(DataContext context, ITicketRepository ticketRepository) : TicketContract.TicketContractBase, ITicketService
{
    private readonly DataContext _context = context;
    private readonly ITicketRepository _ticketRepository = ticketRepository;

    public override async Task<TicketReply> CreateTicket(TicketRequest request, ServerCallContext context)
    {
        try
        {
            var silverTicket = new TicketEntity
            {
                EventId = request.EventId,
                EventName = request.EventName,
                TicketAmount = request.SilverTicketAmount,
                TicketPrice = request.SilverTicketPrice,
                CategoryId = 1
            };

            var goldTicket = new TicketEntity
            {
                EventId = request.EventId,
                EventName = request.EventName,
                TicketAmount = request.GoldTicketAmount,
                TicketPrice = request.GoldTicketPrice,
                CategoryId = 2
            };

            _context.Add(silverTicket);
            _context.Add(goldTicket);
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

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        var entities = await _ticketRepository.GetAllAsync();
        var events = entities.Select(entity => new Ticket
        {
            Id = entity.Id,
            EventId = entity.EventId,
            EventName = entity.EventName,
            TicketAmount = entity.TicketAmount,
            TicketPrice = entity.TicketPrice,
            Category = new Category
            {
                Id = entity.Category.Id,
                CategoryName = entity.Category.CategoryName,
            }

        });

        return events;
    }

    public async Task<IEnumerable<Ticket>> GetTicketByEventIdAsync(string eventId)
    {
        var entities = await _ticketRepository.GetAsync(eventId);
        return entities.Select(entity => new Ticket
        {
            Id = entity.Id,
            EventId = entity.EventId,
            EventName = entity.EventName,
            TicketAmount = entity.TicketAmount,
            TicketPrice = entity.TicketPrice,
            Category = new Category
            {
                Id = entity.Category.Id,
                CategoryName = entity.Category.CategoryName,
            }
        });
    }
}
