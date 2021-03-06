using System.Linq;
using hattrick_full.Models;
using hattrick_full.Providers;

namespace hattrick_full.Services
{
    public class WalletService : IWalletProvider
    {
        private readonly AppContext _context;
        public WalletService(AppContext context)
        {
            _context = context;
        }

        public Wallet GetFunds()
        {
            return _context.Wallets
                .FirstOrDefault();
        }

        public int UpdateFunds(int stake)
        {
            var entity = _context.Wallets
                .FirstOrDefault();

            if (entity != null) {
                entity.Funds -= stake;
                _context.SaveChanges();
            }
            return entity.Funds;
        }
    }
}
