using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UDD.TicketsAPI.Data;
using UDD.TicketsAPI.Models;

namespace UDD.TicketsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketsController(AppDbContext context) => _context = context;

    // GET: api/tickets - Obtiene todos los tickets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets() =>
        await _context.Tickets.ToListAsync();

    // GET: api/tickets/{id} - Busca un ticket por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        await _context.Tickets.FindAsync(id) is Ticket t ? Ok(t) : NotFound();

    // POST: api/tickets - Crea un nuevo ticket
    [HttpPost]
    public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    // PUT: api/tickets/5 - Actualiza un ticket existente
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
    {
        // Permitir que el body no incluya el Id. Si viene, debe coincidir.
        if (ticket.Id != 0 && id != ticket.Id) return BadRequest();
        ticket.Id = id;

        // Busca el ticket existente en la base de datos
        var existingTicket = await _context.Tickets.FindAsync(id);
        if (existingTicket == null) return NotFound();

        // Actualiza las propiedades manualmente para evitar conflictos de rastreo
        existingTicket.Title = ticket.Title;
        existingTicket.Description = ticket.Description;
        existingTicket.Status = ticket.Status;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Tickets.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }
}