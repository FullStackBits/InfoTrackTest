using System;
using System.Collections.Generic;
using System.Text;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Models;

namespace XUnitTests.Mocks
{
    public class MockProcessingQueue : IURLProcessingQueue
    {
        private int processingCount = 0;
        private Queue<SearchRequest> _urls = new Queue<SearchRequest>();

        public int EnQueueLimit { get; set; }

        public bool EnQueue(SearchRequest request)
        {
            if (processingCount < EnQueueLimit)
            {
                processingCount++;
                _urls.Enqueue(request);
                return true;
            }

            return false;
        }

        public SearchRequest DeQueue()
        {
            return _urls.Dequeue();
        }
    }
}
