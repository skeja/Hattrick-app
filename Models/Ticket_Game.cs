namespace hattrick_full.Models
{
    public class Ticket_Game
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
