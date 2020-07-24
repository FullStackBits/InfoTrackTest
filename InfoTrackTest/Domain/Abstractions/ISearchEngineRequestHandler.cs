using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTrackTest.Models;

namespace InfoTrackTest.Domain.Abstractions
{
    public interface ISearchEngineRequestHandler
    {
        Task HandleRequest(SearchEngineRequest request);
    }
}
