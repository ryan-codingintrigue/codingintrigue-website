using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IBlogEntryService
    {
        Task<BlogEntry> GetBlogEntryById(string id, CancellationToken cancellationToken);
        Task<BlogEntryResult> GetBlogEntriesAsync(int page, string contentTypeId, CancellationToken cancellationToken);
        Task<BlogEntryResult> SearchBlogEntriesAsync(string query, int page, CancellationToken cancellationToken);
    }
}