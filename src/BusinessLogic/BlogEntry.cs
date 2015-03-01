using System;

namespace BusinessLogic
{
    public class BlogEntry
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishDateTime { get; set; }

        public string Body { get; set; }
    }
}