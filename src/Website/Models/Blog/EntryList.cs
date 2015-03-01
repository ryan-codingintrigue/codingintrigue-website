using System;
using System.Collections.Generic;

namespace Website.Models.Blog
{
    public class EntryList
    {
        public int TotalPages { get; set; }
        public IEnumerable<Entry> Entries { get; set; }
    }
}