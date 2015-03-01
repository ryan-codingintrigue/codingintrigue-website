using System;
using BusinessLogic;
using Contentful.NET;
using MarkdownDeep;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.Cache.Redis;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace Website
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new {controller = "Blog", action = "Index"});
            });
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
	        var configuration = new Configuration();
	        configuration.AddJsonFile("config.json");
	        serviceCollection.AddTransient<IConfiguration>(factory => configuration);
            serviceCollection.AddTransient<Markdown>();
	        serviceCollection.AddTransient<IMarkdownParser, MarkdownParser>();
            serviceCollection.AddTransient(factory => new RedisCache(new RedisCacheOptions
            {
                Configuration = configuration.Get("redis:connection_string"),
                InstanceName = configuration.Get("redis:name")
            }));
            serviceCollection.AddTransient<IContentfulClient>(factory => new ContentfulClient(configuration.Get("contentful:access_token"),
                configuration.Get("contentful:space_id")));
            serviceCollection.AddTransient<IBlogEntryService, BlogEntryService>();
	        serviceCollection.AddTransient<IAboutMeService, AboutMeService>();
            serviceCollection.AddTransient<ICacheProvider, CacheProvider>();
            serviceCollection.AddMvc();
        }
    }
}
