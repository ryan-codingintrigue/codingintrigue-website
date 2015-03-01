using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contentful.NET;
using Contentful.NET.DataModels;
using Contentful.NET.Search;
using Contentful.NET.Search.Enums;
using Contentful.NET.Search.Filters;

namespace BusinessLogic
{
    public class BlogEntryService : IBlogEntryService
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IContentfulClient _contentfulClient;
	    private readonly IMarkdownParser _markdownParser;

	    public BlogEntryService(ICacheProvider cacheProvider, IContentfulClient contentfulClient, IMarkdownParser markdownParser)
        {
            _cacheProvider = cacheProvider;
            _cacheProvider.Connect();
            _contentfulClient = contentfulClient;
		    _markdownParser = markdownParser;
        }

        public async Task<BlogEntry> GetBlogEntryById(string id, CancellationToken cancellationToken)
        {
            var entry = await _contentfulClient.GetAsync<Entry>(cancellationToken, id);
            return GetBlogEntryFromContentfulEntry(entry);
        }

        public async Task<BlogEntryResult> GetBlogEntriesAsync(int page, string contentTypeId, CancellationToken cancellationToken)
        {
            if (page == 1)
            {
                var cachedEntries = _cacheProvider.GetCachedBlogEntries();
                if (cachedEntries != null && cachedEntries.Items.Any()) return cachedEntries;
            }
            var result =
                await
                    _contentfulClient.SearchAsync<Entry>(cancellationToken,
						new []
						{
							new EqualitySearchFilter(BuiltInProperties.ContentType, contentTypeId),
						 
						},
                        BuiltInProperties.SysUpdatedAt, OrderByDirection.Descending,
                        (page - 1)*10, 10);
            var blogEntries = GetBlogEntryResultFromSearchResult(result);
            if(page == 1) _cacheProvider.SetCachedBlogEntries(blogEntries);
            return blogEntries;
        }

        public async Task<BlogEntryResult> SearchBlogEntriesAsync(string query, int page, CancellationToken cancellationToken)
        {
            var result = await _contentfulClient.SearchAsync<Entry>(cancellationToken, new[]
            {
                new FullTextSearchFilter(query),
            }, skip: (page - 1)*10, limit: 10);
            return GetBlogEntryResultFromSearchResult(result);
        }

        private BlogEntryResult GetBlogEntryResultFromSearchResult(SearchResult<Entry> result)
        {
            return new BlogEntryResult
            {
                Items = result.Items.Select(GetBlogEntryFromContentfulEntry),
                TotalPages = (int) Math.Ceiling((decimal)result.Total/10)
            };
        }

        private BlogEntry GetBlogEntryFromContentfulEntry(Entry entry)
        {
            return new BlogEntry
            {
                Id = entry.SystemProperties.Id,
                Body = _markdownParser.GetHtmlFromMarkdown(entry.GetString("content")),
                PublishDateTime = entry.SystemProperties.CreatedDateTime,
                Title = entry.GetString("title")
            };
        }
    }
}