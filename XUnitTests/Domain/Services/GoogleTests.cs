using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfoTrackTest.Domain.Services;
using Xunit;
using XUnitTests.Mocks;

namespace XUnitTests.Domain.Services
{
    public class GoogleTests
    {
        [Fact]
        public async void Must_Find_1_And_33_Page()
        {
            MockGoogleWebClient webClient = new MockGoogleWebClient();
            webClient.SuccessPages = new int[] {1, 33};
            webClient.UrlToScan = "www.infotrack.com.au";

            MockProcessingQueue queu = new MockProcessingQueue();
            queu.EnQueueLimit = 100;
            MockSearchResponseRepository responseRepository = new MockSearchResponseRepository();

            Google googleEngineService = new Google(queu, responseRepository, webClient);
            await googleEngineService.SearchEngineForRequest(
                new InfoTrackTest.Models.SearchEngineRequest()
                {
                    SearchPhrase = "online title search",
                    SiteURL = "www.infotrack.com.au"
                });

            var results = responseRepository.GetResults(response => response.Found)
                .Select(response => response.Request.Page).ToArray();
            Assert.NotNull(results);
            Assert.Equal(results.Length, webClient.SuccessPages.Length);
            foreach (int page in webClient.SuccessPages)
            {
                Assert.Contains(page, results);
            }
        }

        [Fact]
        public async void Must_Not_Find_Any_Page()
        {
            MockGoogleWebClient webClient = new MockGoogleWebClient();
            webClient.SuccessPages = new int[] { };
            webClient.UrlToScan = "www.infotrack.com.au";

            MockProcessingQueue queu = new MockProcessingQueue();
            queu.EnQueueLimit = 100;
            MockSearchResponseRepository responseRepository = new MockSearchResponseRepository();

            Google googleEngineService = new Google(queu, responseRepository, webClient);
            await googleEngineService.SearchEngineForRequest(
                new InfoTrackTest.Models.SearchEngineRequest()
                {
                    SearchPhrase = "online title search",
                    SiteURL = "www.infotrack.com.au"
                });

            var results = responseRepository.GetResults(response => response.Found)
                .Select(response => response.Request.Page).ToArray();
            Assert.NotNull(results);
            Assert.Equal(0, results.Length);
        }
    }
}
