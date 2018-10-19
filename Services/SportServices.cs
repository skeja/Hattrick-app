using hattrick_full.Models;
using System.Linq;

namespace hattrick_full.Services
{
    public class SportService
    {
        private readonly AppContext _context;
        public SportService(AppContext context)
        {
            _context = context;
        }
        public int CountAll() {
        	return _context.Sports.Count();
        }
}
}
