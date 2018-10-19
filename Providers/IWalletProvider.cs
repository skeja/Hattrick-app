using hattrick_full.Models;

namespace hattrick_full.Providers
{
    public interface IWalletProvider
    {
        Wallet GetFunds();
        int UpdateFunds(int funds);
    }
}
