using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoTrackTest.Models;

namespace InfoTrackTest.Infrastructure.Abstractions
{
    public interface IURLProcessingQueue
    {
        public int EnQueueLimit { get; set; }
        bool EnQueue(SearchRequest request);

        SearchRequest DeQueue();
    }
}
