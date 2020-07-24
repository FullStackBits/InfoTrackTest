using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Models;

namespace XUnitTests.Mocks
{
    public class MockSearchResponseRepository : IURLSearchResponseRepository
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
