using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Website.Models.Blog;

namespace Website.ViewComponents
{
    public class SidebarComponent : ViewComponent
    {
        private readonly IBlogEntryService _blogEntryService;
	    private readonly IConfiguration _configuration;

	    public SidebarComponent(IBlogEntryService blogEntryService, IConfiguration configuration)
	    {
		    _blogEntryService = blogEntryService;
		    _configuration = configuration;
	    }

	    public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _blogEntryService.GetBlogEntriesAsync(1, _configuration.Get("contentful:blog_content_type_id"), new CancellationToken());
            return View(items.Items.Select(i => new Entry
            {
                Date = i.PublishDateTime,
                Id = i.Id,
                Title = i.Title
            }));
        }
    }
}