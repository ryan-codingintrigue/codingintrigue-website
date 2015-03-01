using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Website.Models.Blog;

namespace Website.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogEntryService _blogEntryService;
	    private readonly IConfiguration _configuration;

	    public BlogController(IBlogEntryService blogEntryService, IConfiguration configuration)
	    {
		    _blogEntryService = blogEntryService;
		    _configuration = configuration;
	    }

	    // GET: /<controller>/
        public async Task<IActionResult> Index(CancellationToken cancellationToken, int page = 1)
        {
            var result = await _blogEntryService.GetBlogEntriesAsync(page, _configuration.Get("contentful:blog_content_type_id"), cancellationToken);
            return GetListingResultForBlogEntryResult(result);
        }

        public async Task<IActionResult> Search(CancellationToken cancellationToken, string query, int page = 1)
        {
            var result = await _blogEntryService.SearchBlogEntriesAsync(query, page, cancellationToken);
            return GetListingResultForBlogEntryResult(result);
        }

        public async Task<IActionResult> Detail(CancellationToken cancellationToken, string id)
        {
            var result = await _blogEntryService.GetBlogEntryById(id, cancellationToken);
            return View(GetBlogEntryFromResult(result));
        }

        private IActionResult GetListingResultForBlogEntryResult(BlogEntryResult result)
        {
            var items = result.Items.Select(GetBlogEntryFromResult);
            return View("Index", new EntryList
            {
                Entries = items,
                TotalPages = result.TotalPages
            });
        }

        private static Entry GetBlogEntryFromResult(BlogEntry entry)
        {
            return new Entry
            {
                Id = entry.Id,
                Date = entry.PublishDateTime,
                Markdown = entry.Body,
                Title = entry.Title
            };
        }
    }
}
