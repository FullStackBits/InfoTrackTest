using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTrackTest.Models;

namespace InfoTrackTest.Infrastructure.Abstractions
{
    public interface IURLSearchResponseRepository
    {
        public void Add(SearchResponse request);
        public List<SearchResponse> GetResults(Func<SearchResponse, bool> criteria);
    }
}
