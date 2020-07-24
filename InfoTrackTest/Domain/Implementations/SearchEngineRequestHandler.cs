using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using InfoTrackTest.Domain.Abstractions;
using InfoTrackTest.Domain.Services;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Models;
using Microsoft.Extensions.Configuration;

namespace InfoTrackTest.Domain.Implementations
{
    public class SearchEngineRequestHandler : ISearchEngineRequestHandler
    {
        IConfiguration _configuration;
        SearchEngineFactory _searchEngineFactory;
        public SearchEngineRequestHandler(IConfiguration configuration, SearchEngineFactory searchEngineFactory) => (_configuration, _searchEngineFactory) = (configuration, searchEngineFactory);

        public async Task HandleRequest(SearchEngineRequest request)
        {
            var searchEnginers = _configuration.GetSection("SearchEngines").Get<SearchEngineSpecification[]>();
            foreach (var searchEngine in searchEnginers)
            {
                await _searchEngineFactory.GetService(searchEngine).SearchEngineForRequest(request);
            }
        }

    }
}
