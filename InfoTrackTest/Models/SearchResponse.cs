using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackTest.Models
{
    public class SearchResponse
    {
        public SearchRequest Request { get; set; }
        public bool Found { get; set; }
    }
}
