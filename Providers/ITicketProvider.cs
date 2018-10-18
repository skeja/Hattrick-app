using System.Collections.Generic;
using hattrick_full.Models;

namespace hattrick_full.Providers
{
    public interface ITicketProvider
    {
        Ticket GetLast();

        List<Ticket_Game> GetByTicketId(int id);
        int Add(Ticket newTicket);
        int AddGame(Ticket_Game newGame);
    }
}
