using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Models;

namespace InfoTrackTest.Infrastructure.Implementations
{
    public class URLSearchResponseRepository : IURLSearchResponseRepository
    {
        List<SearchResponse> _searchResults = new List<SearchResponse>();
        public void Add(SearchResponse searchResult)
        {
            _searchResults.Add(searchResult);
        }

        public List<SearchResponse> GetResults(Func<SearchResponse, bool> criteria)
        {
            return _searchResults.Where(criteria).ToList();
        }
        }
}
