using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class BlogEntryResult
    {
        public IEnumerable<BlogEntry> Items { get; set; }
        public int TotalPages { get; set; }
    }
}