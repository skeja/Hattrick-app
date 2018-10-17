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

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet() { Id = 1, Funds = 100 }
            );

            modelBuilder.Entity<Sport>().HasData(
                new Sport() { Id = 1, Name = "Football" },
                new Sport() { Id = 2, Name = "Basketball" },
                new Sport() { Id = 3, Name = "HandBall" },
                new Sport() { Id = 4, Name = "Tennis" },
                new Sport() { Id = 5, Name = "Hockey" }
            );
            modelBuilder.Entity<League>().HasData(
                new League() { Id = 1, Name = "England 1.", SportId = 1 },
                new League() { Id = 2, Name = "France 1.", SportId = 1 },
                new League() { Id = 3, Name = "Spain 1.", SportId = 1 },
                new League() { Id = 4, Name = "German 1.", SportId = 1 },
                new League() { Id = 5, Name = "Croatia 1.", SportId = 1 },
                new League() { Id = 6, Name = "Adriatic league 1.", SportId = 2 },
                new League() { Id = 7, Name = "Turkey 1.", SportId = 2 },
                new League() { Id = 8, Name = "Spain 1.", SportId = 2 },
                new League() { Id = 9, Name = "German 1.", SportId = 2 },
                new League() { Id = 10, Name = "Italy 1.", SportId = 2 },
                new League() { Id = 11, Name = "Champions League", SportId = 3 },
                new League() { Id = 12, Name = "US Open", SportId = 4 },
                new League() { Id = 13, Name = "NHL", SportId = 5 },
                new League() { Id = 14, Name = "AHL", SportId = 5 }
            );
            modelBuilder.Entity<Bonus>().HasData(
                new Bonus() { Id = 1, Extra = 5 },
                new Bonus() { Id = 2, Extra = 10 }
            );
            modelBuilder.Entity<Game>().HasData(
                new Game() { Id = 1, Name = "Arsenal - Leicester", Home = 1.50m, Draw = 4.30m, Guest = 6.00m, LeagueId = 1 },
                new Game() { Id = 2, Name = "Cardif - Fulham", Home = 2.35m, Draw = 3.20m, Guest = 3.00m, LeagueId = 1 },
                new Game() { Id = 3, Name = "Burnemouth - Southampton", Home = 2.15m, Draw = 3.20m, Guest = 3.40m, LeagueId = 1 },
                new Game() { Id = 4, Name = "Lyon - O. Nimes", Home = 1.35m, Draw = 4.80m, Guest = 8.00m, LeagueId = 2 },
                new Game() { Id = 5, Name = "Strasbourg - Monaco", Home = 2.50m, Draw = 3.20m, Guest = 2.80m, LeagueId = 2 },
                new Game() { Id = 6, Name = "Ream Madrid - Levante", Home = 1.15m, Draw = 7.00m, Guest = 17.00m, LeagueId = 3 },
                new Game() { Id = 7, Name = "Eibar - Ath. Bilbao", Home = 2.75m, Draw = 3.00m, Guest = 2.65m, LeagueId = 3 },
                new Game() { Id = 8, Name = "R Sociedat - Girona", Home = 1.70m, Draw = 3.50m, Guest = 5.00m, LeagueId = 3 },
                new Game() { Id = 9, Name = "Wolfsburg - Bayern", Home = 8.30m, Draw = 4.80m, Guest = 1.35m, LeagueId = 4 },
                new Game() { Id = 10, Name = "Stuttgart - B. Dortmund", Home = 4.10m, Draw = 3.50m, Guest = 1.85m, LeagueId = 4 },
                new Game() { Id = 11, Name = "B Mgladbach - Mainz", Home = 1.65m, Draw = 3.80m, Guest = 4.90m, LeagueId = 4 },
                new Game() { Id = 12, Name = "Inter zapresic - Rijeka", Home = 4.50m, Draw = 3.40m, Guest = 1.80m, LeagueId = 5 },
                new Game() { Id = 13, Name = "Istra 1961 - D. Zagreb", Home = 12.00m, Draw = 5.30m, Guest = 1.25m, LeagueId = 5 },
                new Game() { Id = 14, Name = "Sl. Belupo - HNK Gorica", Home = 2.30m, Draw = 3.20m, Guest = 3.10m, LeagueId = 5 },
                new Game() { Id = 15, Name = "Lokomotiva - Hajduk", Home = 2.60m, Draw = 3.20m, Guest = 2.70m, LeagueId = 5 },
                new Game() { Id = 16, Name = "Crvena Zvezda - Krka", Home = 1.05m, Draw = 19.00m, Guest = 9.50m, LeagueId = 6 },
                new Game() { Id = 17, Name = "Cedevita - Mega Bemax", Home = 1.60m, Draw = 17.00m, Guest = 4.60m, LeagueId = 6 },
                new Game() { Id = 18, Name = "Giessen - Eisbaren B.", Home = 1.45m, Draw = 16.00m, Guest = 2.80m, LeagueId = 9 },
                new Game() { Id = 19, Name = "Bonn  Beyreuth", Home = 1.45m, Draw = 16.00m, Guest = 2.80m, LeagueId = 9 },
                new Game() { Id = 20, Name = "Unicaja - Gran Canaria", Home = 1.40m, Draw = 16.00m, Guest = 3.00m, LeagueId = 8 },
                new Game() { Id = 21, Name = "Bahcesehir Koleji - Galatasaray", Home = 2.30m, Draw = 14.00m, Guest = 1.65m, LeagueId = 7 },
                new Game() { Id = 22, Name = "Bologna - Milano", Home = 2.65m, Draw = 15m, Guest = 1.50m, LeagueId = 10 },
                new Game() { Id = 23, Name = "Montpellier - Veszprem", Home = 2.15m, Draw = 7.50m, Guest = 1.95m, LeagueId = 11 },
                new Game() { Id = 24, Name = "Kielce - Vardar", Home = 1.55m, Draw = 9.00m, Guest = 2.85m, LeagueId = 11 },
                new Game() { Id = 25, Name = "Besiktas- B. SIlkeborg", Home = 5.10m, Draw = 13.00m, Guest = 1.20m, LeagueId = 11 },
                new Game() { Id = 26, Name = "Tatran Presov - Sporting", Home = 1.70m, Draw = 9.00m, Guest = 2.45m, LeagueId = 11 },
                new Game() { Id = 27, Name = "Zaporozhye - PPD Zagreb", Home = 1.50m, Draw = 10.00m, Guest = 2.90m, LeagueId = 11 },
                new Game() { Id = 28, Name = "Pavic - Klein", Home = 1.45m, Guest = 2.40m, LeagueId = 12 },
                new Game() { Id = 29, Name = "Rodinov - Ignatik", Home = 1.65m, Guest = 2.20m, LeagueId = 12 },
                new Game() { Id = 30, Name = "Rosol - Gakhov", Home = 1.15m, Guest = 4.7m, LeagueId = 12 },
                new Game() { Id = 31, Name = "Hepbert - Dokovic", Home = 2.90m, Guest = 1.10m, LeagueId = 12 },
                new Game() { Id = 32, Name = "Innsbruck - Medvescak", Home = 1.90m, Draw = 5.50m, Guest = 3.70m, LeagueId = 14 },
                new Game() { Id = 33, Name = "Fehervar - Graz", Home = 1.80m, Draw = 4.40m, Guest = 3.19m, LeagueId = 14 },
                new Game() { Id = 34, Name = "Linz - Villach", Home = 1.85m, Draw = 4.40m, Guest = 2.90m, LeagueId = 14 },
                new Game() { Id = 35, Name = "Koln - Manheim", Home = 2.10m, Draw = 4.40m, Guest = 2.50m, LeagueId = 13 },
                new Game() { Id = 36, Name = "Augsburg - Wolfsburg", Home = 2.10m, Draw = 4.40m, Guest = 2.50m, LeagueId = 13 }
            );
        }
    }


}

