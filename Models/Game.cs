using System.Collections.Generic;

namespace hattrick_full.Models {
    public class Game {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Home { get; set; }
        public decimal Draw { get; set; }
        public decimal Guest { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }
        public List<Ticket_Game> Ticket_Games { get; set; }
    }
}
