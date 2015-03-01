using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MarkdownDeep;
using Microsoft.AspNet.Mvc;
using Website.Models.Blog;

namespace Website.ViewComponents
{
    public class BlogEntryComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Entry entry)
        {
            return View(entry);
        }
    }
}