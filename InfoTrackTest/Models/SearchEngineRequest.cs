using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackTest.Models
{
    public class SearchEngineRequest
    {
        public string SearchPhrase { get; set; }
        public string SiteURL { get; set; }

        public SearchEngineRequest()
        {
        }

        public SearchEngineRequest(string searchPhrase, string siteURL) =>
            (SearchPhrase, SiteURL) = (searchPhrase, siteURL);
    }
}
