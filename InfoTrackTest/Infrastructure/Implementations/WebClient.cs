using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InfoTrackTest.Infrastructure.Abstractions;

namespace InfoTrackTest.Infrastructure.Implementations
{
    public class WebClient : IWebClient
    {
        public async Task<string> GetStringAsync(string URL)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(URL);
        }
    }
}
