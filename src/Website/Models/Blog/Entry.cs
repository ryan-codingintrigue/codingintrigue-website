using System;

namespace Website.Models.Blog
{
    public class Entry
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Markdown { get; set; }
        public DateTime? Date { get; set; }
    }
}