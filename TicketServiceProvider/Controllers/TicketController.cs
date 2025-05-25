using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketServiceProvider.Business.Services;

namespace TicketServiceProvider.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController(ITicketService ticketService) : ControllerBase
{
    private readonly ITicketService _ticketService = ticketService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _ticketService.GetAllTicketsAsync();
        return Ok(events);
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> Get(string eventId)
    {
        var events = await _ticketService.GetTicketByEventIdAsync(eventId);
        return events == null ? NotFound() : Ok(events);
    }


}
