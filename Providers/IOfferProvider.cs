using System.Collections.Generic;
using hattrick_full.Models;

namespace hattrick_full.Providers
{
    public interface IOfferProvider
    {
        IEnumerable<Game> GetOffer();
    }
}
