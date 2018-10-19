using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hattrick_full.Models
{
    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Stake { get; set; }
        public int Odd { get; set; }
        public bool IsBetted { get; set; }
        public int BonusId { get; set; }
        public Bonus Bonus { get; set; }
        public List<Ticket_Game> Ticket_Games { get; set; }
    }
}
