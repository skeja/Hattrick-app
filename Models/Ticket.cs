namespace hattrick_full.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Stake { get; set; }
        public int Odd { get; set; }
        public int BonusId { get; set; }
        public Bonus Bonus { get; set; }
    }
}
