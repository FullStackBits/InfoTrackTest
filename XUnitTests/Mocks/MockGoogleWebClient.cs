using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackTest.Infrastructure.Abstractions;

namespace XUnitTests.Mocks
{
    public class MockGoogleWebClient : IWebClient
    {
        private int CurrentPage = 1;

        public int[] SuccessPages { get; set; }
        public string UrlToScan { get; set; }
        public Task<string> GetStringAsync(string URL)
        {
            string pageContents = GeneratePageContent(CurrentPage);
            CurrentPage++;
            return Task.FromResult(pageContents);
        }

        private string GeneratePageContent(int pageNumber)
        {
            return $@"
                <div id=""search""> 
                    <div id=""g""> 
                        <div id=""r""> 
                            <a href=""https://{PageBasedContents(pageNumber)}/"" ping = ""/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://www.landata.vic.gov.au/&amp;ved=2ahUKEwj3qbWLoOTqAhUrlEsFHZJQCOs4ChAWMAB6BAgBEAE"">
                                < br />
                                <h3 class=""LC20lb DKV0Md"">Landata</h3>
                                <div class=""TbwUpd NJjxre"">
                                    <cite class=""iUh30 tjvcx"">
                                        {PageBasedContents(pageNumber)}
                                    <span class=""eipWBe""></span>
                                </cite>
                            </div>
                            </a>
                        </div>
                    </div>
                </div>
                ";

            string PageBasedContents(int pageNumber) =>
                SuccessPages.Contains(pageNumber) ? UrlToScan : "Dummy Text";
        }
    }
}
