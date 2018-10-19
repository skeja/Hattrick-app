using System;
using hattrick_full.Models;
using hattrick_full.Providers;
using Microsoft.AspNetCore.Mvc;

namespace hattrick_full.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketProvider ticketProvider;
        public TicketController(ITicketProvider ticketProvider)
        {
            this.ticketProvider = ticketProvider;
        }

        [HttpGet("[action]")]
        public IActionResult Find([FromQuery]int id)
        {
            var result = ticketProvider.GetByTicketId(id);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public IActionResult Last()
        {
            var ticket = ticketProvider.GetLast();
            return Ok(ticket);
        }

        [HttpGet("[action]")]
        public IActionResult GetBetted()
        {
            var ticket = ticketProvider.GetBetted();
            return Ok(ticket);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]Ticket newTicket)
        {
            ticketProvider.Add(newTicket);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Add([FromBody]Ticket_Game newGame)
        {
            ticketProvider.AddGame(newGame);
            return Ok();
        }

        [HttpDelete("[action]/{TicketId:int}/{GameId:int}")]
        public IActionResult Delete(int TicketId,int GameId)
        {
            ticketProvider.RemoveGame(TicketId, GameId);
            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult UpdateGame([FromBody]Ticket_Game game)
        {
            ticketProvider.UpdateGame(game);
            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult UpdateTicket([FromBody]Ticket ticket)
        {
            ticketProvider.UpdateTicket(ticket);
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult GetBonus([FromQuery]int TicketId)
        {
            var result = ticketProvider.GetBonus(TicketId);
            return Ok(result);
        }
    }
}
