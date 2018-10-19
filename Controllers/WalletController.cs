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

        [HttpPut("[action]")]
        public IActionResult UpdateFunds([FromBody]int funds)
        {
            var result = walletProvider.UpdateFunds(funds);
            return Ok(result);
        }
    }
}
