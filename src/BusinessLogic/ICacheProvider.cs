using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface ICacheProvider
    {
        void Connect();
        BlogEntryResult GetCachedBlogEntries();
        IEnumerable<BlogEntry> GetCachedRecentEntries(); 
        void SetCachedBlogEntries(BlogEntryResult blogEntries);
        void SetCachedRecentEntries(IEnumerable<BlogEntry> blogEntries);
    }
}