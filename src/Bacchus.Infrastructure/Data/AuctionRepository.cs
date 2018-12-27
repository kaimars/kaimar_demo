using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bacchus.Core.Entities;
using Bacchus.Core.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Bacchus.Infrastructure.Data
{
    public class AuctionRepository: IListRepository<Auction>
    {
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        public Task<IReadOnlyList<Auction>> ListAllAsync()
        {
            using (var httpClient = new WebClient())
            {
                var jsonData = httpClient.DownloadString("http://uptime-auction-api.azurewebsites.net/api/Auction");
                var result = JsonConvert.DeserializeObject<Auction[]>(jsonData, _jsonSettings);
                return Task.FromResult<IReadOnlyList<Auction>>(new List<Auction>(result).AsReadOnly());
            }
        }
    }
}