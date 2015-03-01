using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using Website.Models.About;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Website.Controllers
{
    public class AboutMeController : Controller
    {
	    private readonly IAboutMeService _aboutMeService;
	    private readonly IConfiguration _configuration;

	    public AboutMeController(IAboutMeService aboutMeService, IConfiguration configuration)
	    {
		    _aboutMeService = aboutMeService;
		    _configuration = configuration;
	    }

	    // GET: /<controller>/
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
	        var entry = await _aboutMeService.GetAboutItem(cancellationToken, _configuration.Get("contentful:about_me_id"));
            return View(new About
            {
	            Title = entry.Title,
				Body = entry.Body
            });
        }
    }
}
