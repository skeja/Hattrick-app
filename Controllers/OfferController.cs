using hattrick_full.Providers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace hattrick_full.Controllers
{
    [Route("api/[controller]")]
    public class OfferController : Controller
    {
        private readonly IOfferProvider gameProvider;
        public OfferController(IOfferProvider gameProvider)
        {
            this.gameProvider = gameProvider;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            var allGames = gameProvider.GetOffer();

            return Ok(allGames);
        }
    }
}
