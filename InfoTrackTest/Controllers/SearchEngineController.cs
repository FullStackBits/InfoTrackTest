using System;
using System.Linq;
using System.Threading.Tasks;
using InfoTrackTest.Domain.Abstractions;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackTest.Controllers
{
    public class SearchEngineController : Controller
    {
        private readonly ISearchEngineRequestHandler _requestHandler;
        private readonly IURLSearchResponseRepository _urlSearchResponseRepositry;

        public SearchEngineController(ISearchEngineRequestHandler requestHandler,
            IURLSearchResponseRepository urlSearchResponseRepository) =>
            (_requestHandler, _urlSearchResponseRepositry) = (requestHandler, urlSearchResponseRepository);


        public IActionResult Index()
        {
            return View(new SearchEngineRequest()
            {
                SearchPhrase = "online title search",
                SiteURL = "www.infotrack.com.au"
            });
        }

        [HttpPost]
        public async Task<ActionResult> Search(SearchEngineRequest request)
        {
            try
            {
                await _requestHandler.HandleRequest(request);
            }
            catch (Exception exp)
            {
            }

            var result = _urlSearchResponseRepositry.GetResults(resp => resp.Found).Select(resp => resp.Request.Page)
                .ToArray();
            return View(result);
        }
    }
}
