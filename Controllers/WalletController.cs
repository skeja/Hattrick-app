using hattrick_full.Providers;
using Microsoft.AspNetCore.Mvc;

namespace hattrick_full.Controllers
{
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly IWalletProvider walletProvider;
        public WalletController(IWalletProvider walletProvider)
        {
            this.walletProvider = walletProvider;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            var result = walletProvider.GetFunds();
            return Ok(result);
        }

        [HttpPut("[action]/{stake:int}")]
        public IActionResult UpdateFunds(int stake)
        {
            var result = walletProvider.UpdateFunds(stake);
            return Ok(result);
        }
    }
}
