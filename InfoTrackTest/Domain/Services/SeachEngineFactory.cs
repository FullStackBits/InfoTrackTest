using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTrackTest.Domain.Abstractions;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace InfoTrackTest.Domain.Services
{
    public class SearchEngineFactory
    {

        private readonly IURLProcessingQueue _urlProcessingQueue;
        private readonly IURLSearchResponseRepository _urlSearchRepository;
        private readonly IWebClient _webClient;

        public SearchEngineFactory(IURLProcessingQueue urlProcessingQueue, IURLSearchResponseRepository urlSearchRepository, IWebClient webClient) => 
            (_urlProcessingQueue, _urlSearchRepository, _webClient) = (urlProcessingQueue, urlSearchRepository, webClient);

        public ISearchEngineService GetService(SearchEngineSpecification specification)
        {
            _urlProcessingQueue.EnQueueLimit = specification.PagesToScan;
            return specification.Name.ToLower() switch
            {
                "google" => new Google(_urlProcessingQueue, _urlSearchRepository, _webClient),
                _ => throw new NotImplementedException()
            };
        }
    }
}
