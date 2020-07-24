using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackTest.Infrastructure.Abstractions
{
    public  interface IWebClient
    {
        Task<String> GetStringAsync(string URL);
    }
}
