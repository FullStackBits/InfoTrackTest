using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTrackTest.Infrastructure.Abstractions;
using InfoTrackTest.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace InfoTrackTest.Infrastructure.Implementations
{
    public class URLProcessingQueue : IURLProcessingQueue
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
