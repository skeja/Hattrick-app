using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using hattrick_full.Models;
using hattrick_full.Providers;

namespace hattrick_full.Services
{

    public class OfferService : IOfferProvider
    {
        private AppContext _context;

        public OfferService(AppContext context) {
            _context = context;
        }
        public IEnumerable<Game> GetOffer()
        {
            return _context.Games
                .Include(game => game.League)
                .ThenInclude(league => league.Sport );
        }
    }
}
