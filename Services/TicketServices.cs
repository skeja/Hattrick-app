using System.Collections.Generic;
using System.Linq;
using hattrick_full.Models;
using hattrick_full.Providers;
using Microsoft.EntityFrameworkCore;

namespace hattrick_full.Services
{
    public class TicketService : ITicketProvider
    {
        private AppContext _context;
        public TicketService(AppContext context)
        {
            _context = context;
        }

        public int Add(Ticket newTicket)
        {
            _context.Tickets.Add(newTicket);
            _context.SaveChanges();
            return 1;
        }

        public int AddGame(Ticket_Game newGame)
        {
            _context.Ticket_Games.Add(newGame);
            _context.SaveChanges();
            return 1;
        }

        public List<Ticket_Game> GetByTicketId(int TicketId)
        {
            return _context.Ticket_Games
            .Include(tg => tg.Game)
            .Where(tg => tg.TicketId == TicketId)
            .ToList();
        }
        public Ticket GetLast()
        {
            return _context.Tickets
                .LastOrDefault(ticket => ticket.IsBetted == false);
        }

        public void RemoveGame(Ticket_Game game)
        {
            var entity = _context.Ticket_Games
            .FirstOrDefault(tg => tg.TicketId == game.TicketId && tg.GameId == game.GameId);

            if (entity != null) {
                _context.Ticket_Games.Remove(entity);
            }
        }

        public void UpdateGame(Ticket_Game game)
        {
            var entity = _context.Ticket_Games
            .FirstOrDefault(tg => tg.TicketId == game.TicketId && tg.GameId == game.GameId);
            entity.Type = game.Type;
            _context.SaveChanges();
        }

        public int UpdateTicket(Ticket ticket)
        {
            var entity = _context.Tickets.FirstOrDefault(item => item.Id == ticket.Id);
            // update ticket
            entity.IsBetted = ticket.IsBetted;
            _context.SaveChanges();
            return 1;
        }

        public bool HasGamesFromSameSport(int ticketId, int gamesNumber)
        {
            return _context.Ticket_Games
            .Where(tg => tg.TicketId == ticketId)
            .GroupBy(tg => tg.Game.League.SportId)
            .Any(sGroup => sGroup.Count() >= gamesNumber);
        }

        public int SportsCount(int ticketId) {
        	return _context.Ticket_Games
		    .Where(tg => tg.TicketId == ticketId)
		    .GroupBy(tg => tg.Game.League.SportId)
		    .Count();
}
    }
}
