using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InfoTrackTest.Domain.Abstractions;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Models;

namespace InfoTrackTest.Domain.Services
{
    public class Google : ISearchEngineService
    {
        private const string SearchEngineUrl = "https://www.google.com";
        private const string SearchPath = "/search";

        private const string HrefPattern = "<a(.*?)href=\"(?<LinkURL>[^\"]*)\"[^>]*>";

        private readonly IURLProcessingQueue _urlProcessingQueue;
        private readonly IURLSearchResponseRepository _urlSearchRepository;
        private readonly IWebClient _webClient;
        private SearchEngineRequest _request;

        public Google(
            IURLProcessingQueue urlProcessingQueue,
            IURLSearchResponseRepository urlSearchRepository,
            IWebClient webClient) =>
            (_urlProcessingQueue, _urlSearchRepository, _webClient) =
            (urlProcessingQueue, urlSearchRepository, webClient);

        public async Task SearchEngineForRequest(SearchEngineRequest request)
        {
            _request = request;
            string requestSearchUrl = $"{SearchEngineUrl}{SearchPath}?q={WebUtility.UrlEncode(request.SearchPhrase)}";
            _urlProcessingQueue.EnQueue(new SearchRequest() {URL = requestSearchUrl, Page = 1});
            try
            {
                var currentSearchRequest = _urlProcessingQueue.DeQueue();
                do
                {
                    await FindSiteInSearchResults(currentSearchRequest);
                    currentSearchRequest = _urlProcessingQueue.DeQueue();
                } while (currentSearchRequest != null);
            }
            catch (InvalidOperationException _)
            {
                Console.WriteLine("Processing ended");
            }
        }

        private async Task FindSiteInSearchResults(SearchRequest searchRequest)
        {
            var contents = await _webClient.GetStringAsync(searchRequest.URL);
            var linkMatches = Regex.Matches(contents, HrefPattern).ToList();

            if (linkMatches.Any(LinkMatchingCriteria))
            {
                _urlSearchRepository.Add(
                    new SearchResponse() {Request = searchRequest, Found = true}
                );
            }


            _urlProcessingQueue.EnQueue(new SearchRequest()
            {
                URL =
                    $"{SearchEngineUrl}{SearchPath}?q={WebUtility.UrlEncode(_request.SearchPhrase)}&start={10 * searchRequest.Page}",
                Page = searchRequest.Page + 1
            });
        }

        Func<Match, bool> LinkMatchingCriteria => match =>
            match.Groups["LinkURL"].ToString().ToLower().Contains(_request.SiteURL.ToLower());
    }
}
