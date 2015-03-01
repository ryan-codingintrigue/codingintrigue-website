using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNet.Mvc;
using Moq;
using Website.Controllers;
using Website.Models.Blog;
using Xunit;

namespace Website.Tests
{
    public class BlogControllerTests
    {
        [Fact]
        public async Task TestIndexReturnsView()
        {
            var mockBlogEntryService = new Mock<IBlogEntryService>();
            var controller = new BlogController(mockBlogEntryService.Object);
            var result = await controller.Index();
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.Equal("Index", viewResult.ViewName);
        }

        [Fact]
        public async Task TestIndexQueriesBlogServiceForCorrectPage()
        {
            var mockBlogEntryService = new Mock<IBlogEntryService>();
            var controller = new BlogController(mockBlogEntryService.Object);
            var result = await controller.Index(5);
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            var modelData = viewResult.ViewData.Model;
            Assert.NotNull(modelData);
            
            Assert.IsType<IEnumerable<Entry>>(modelData);
        }

         
    }
}
