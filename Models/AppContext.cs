using System;
using Microsoft.EntityFrameworkCore;
using hattrick_full.Models;

namespace hattrick_full.Models {
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Bonus> Bonuses { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Ticket_Game> Ticket_Games { get; set; }
    }


}

