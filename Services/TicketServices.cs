using System;
using System.Collections.Generic;
using System.Linq;
using hattrick_full.Models;
using hattrick_full.Providers;
using Microsoft.EntityFrameworkCore;

namespace hattrick_full.Services
{
    public class TicketService : ITicketProvider
    {
        private Models.AppContext _context;
        public TicketService(Models.AppContext context)
        {
            _context = context;
        }
        private int _defaultBonusId = 3;

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
            UpdateTicket(new Ticket{ Id = newGame.TicketId });
            return 1;
        }

        public List<Ticket_Game> GetByTicketId(int TicketId)
        {
            return _context.Ticket_Games
            .Include(tg => tg.Game)
            .Where(tg => tg.TicketId == TicketId)
            .ToList();
        }
        public List<Ticket_Game> GetBetted()
        {
             return _context.Ticket_Games
             .Include(tg => tg.Game)
             .Include(tg => tg.Ticket)
             .Where(tg => tg.Ticket.IsBetted == true)
             .ToList();
        }
        public Ticket GetLast()
        {
            return _context.Tickets
                .LastOrDefault(ticket => ticket.IsBetted == false);
        }

        public void RemoveGame(int TicketId, int GameId)
        {
            var entity = _context.Ticket_Games
            .Include(tg => tg.Game)
            .Include(tg => tg.Ticket)
            .FirstOrDefault(tg => tg.TicketId == TicketId && tg.GameId == GameId);

            if (entity != null) {
                _context.Ticket_Games.Remove(entity);
                UpdateTicket(new Ticket{ Id = TicketId });
                _context.SaveChanges();
            }
        }

        public void UpdateGame(Ticket_Game game)
        {
            var entity = _context.Ticket_Games
            .FirstOrDefault(tg => tg.TicketId == game.TicketId && tg.GameId == game.GameId);

            if (entity != null) {
                entity.Type = game.Type;
            }
            _context.SaveChanges();
            UpdateTicket(new Ticket{ Id = entity.TicketId });
        }

        public int UpdateTicket(Ticket ticket)
        {
            var entity = _context.Tickets.FirstOrDefault(item => item.Id == ticket.Id);
            entity.IsBetted = ticket.IsBetted;
            entity.Stake = ticket.Stake;
            entity.Odd = ticket.Odd;
            entity.BonusId = GetBonusId(entity.Id);
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

        public int CountAllSports() {
        	return _context.Sports.Count();
        }
        private bool isBonusTen(int ticketId) {
            return SportsCount(ticketId) == CountAllSports();
        }
        public int GetBonusId(int tickedId) {
            Dictionary<int,Func<int,bool>> map = _priorityMap();
	        for(int i = 0; i < map.Count; i++) {
		    if (map.ElementAt(i).Value.Invoke(tickedId)) return map.ElementAt(i).Key;
	        }
	        return _defaultBonusId;
        }
        private bool isBonusFive(int tickedId) {
            return HasGamesFromSameSport(tickedId, 3);
        }
        private Dictionary<int,Func<int,bool>> _priorityMap () {
            return new Dictionary<int,Func<int,bool>> {
                { 2, this.isBonusTen },
                { 1, this.isBonusFive }
            };
        }
        public int GetBonus(int TicketId)
        {
            var ticket = _context.Tickets
                .Include(t => t.Bonus)
                .FirstOrDefault(t => t.Id == TicketId);

            if (ticket != null) {
                return ticket.Bonus.Extra;
            }

            return 0;
        }
    }
}
