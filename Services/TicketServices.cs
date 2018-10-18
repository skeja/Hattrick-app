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
            return 1;
        }

        public Ticket_Game GetById(int id)
        {
            return _context.Ticket_Games
                .Find(id);
            // .Include(tg => tg.TicketId == id);
            // .FirstOrDefault(ticket_game => ticket_game.TicketId == id);
        }
        public Ticket GetLast()
        {
            return _context.Tickets
                .LastOrDefault(ticket => ticket.IsBetted == false);
        }
    }
}
