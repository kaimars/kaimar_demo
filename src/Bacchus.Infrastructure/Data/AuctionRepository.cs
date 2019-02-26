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
    public class AuctionRepository : IListRepository<Auction>
    {
        private const String CACHE_KEY = "AuctionRepository.Auctions";
        private readonly String _serviceAddress;
        private readonly int _cacheDurationSeconds;

        public AuctionRepository(String serviceAddress, int cacheDurationSeconds=60)
        {
            this._serviceAddress = serviceAddress;
            this._cacheDurationSeconds = cacheDurationSeconds;
        }

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
                var jsonData = httpClient.DownloadString(_serviceAddress);
                var result = JsonConvert.DeserializeObject<Auction[]>(jsonData, _jsonSettings);
                return Task.FromResult<IReadOnlyList<Auction>>(new List<Auction>(result).AsReadOnly());
            }
        }

        public async Task<IReadOnlyList<Auction>> ListAllAsync()
        {
            ObjectCache cache = MemoryCache.Default;
            var result = cache[CACHE_KEY] as IReadOnlyList<Auction>;
            if (result == null)
            {
                result = await this.LoadAuctions();
                cache.Set(CACHE_KEY, result, DateTime.Now.AddSeconds(_cacheDurationSeconds));
            }
            return result;
        }
    }
}