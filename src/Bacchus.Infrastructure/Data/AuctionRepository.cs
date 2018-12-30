using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bacchus.Core.Entities;
using Bacchus.Core.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Runtime.Caching;

namespace Bacchus.Infrastructure.Data
{
    public class AuctionRepository: IListRepository<Auction>
    {
        private const String CACHE_KEY = "AuctionRepository.Auctions";

        // TODO: in real application make cache settings configuarable
        private const int CACHE_DURATION_SECONDS = 60;

        // TODO: in real application make URL configuarable
        private const string ServiceAddress = "http://uptime-auction-api.azurewebsites.net/api/Auction";

        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        private Task<IReadOnlyList<Auction>> LoadAuctions()
        {
            using (var httpClient = new WebClient())
            {
                var jsonData = httpClient.DownloadString(ServiceAddress);
                var result = JsonConvert.DeserializeObject<Auction[]>(jsonData, _jsonSettings);
                return Task.FromResult<IReadOnlyList<Auction>>(new List<Auction>(result).AsReadOnly());
            }
        }
        
        public async Task<IReadOnlyList<Auction>> ListAllAsync()
        {
            ObjectCache cache = MemoryCache.Default;
            var result = cache[CACHE_KEY] as IReadOnlyList<Auction>;
            if (result == null){
                result = await this.LoadAuctions();
                cache.Set(CACHE_KEY, result, DateTime.Now.AddSeconds(CACHE_DURATION_SECONDS));
            }
            return result;
        }
    }
}