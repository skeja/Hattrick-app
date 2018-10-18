using hattrick_full.Models;

namespace hattrick_full.Providers
{
    public interface ITicketProvider
    {
        Ticket GetLast();

        Ticket_Game GetById(int id);
        int Add(Ticket newTicket);
        int AddGame(Ticket_Game newGame);
    }
}
