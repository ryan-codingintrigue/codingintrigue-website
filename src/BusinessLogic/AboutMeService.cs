using System;
using System.Threading;
using System.Threading.Tasks;
using Contentful.NET;
using Contentful.NET.DataModels;

namespace BusinessLogic
{
	public class AboutMeService : IAboutMeService
	{
		private readonly IMarkdownParser _markdownParser;
		private readonly IContentfulClient _contentfulClient;

		public AboutMeService(IMarkdownParser markdownParser, IContentfulClient contentfulClient)
		{
			_markdownParser = markdownParser;
			_contentfulClient = contentfulClient;
		}

		public async Task<AboutItem> GetAboutItem(CancellationToken cancellationToken, string entryId)
		{
			var entry = await _contentfulClient.GetAsync<Entry>(cancellationToken, entryId);
			return new AboutItem
			{
				Body = _markdownParser.GetHtmlFromMarkdown(entry.GetString("body")),
				Title = entry.GetString("title")
			};
		}
	}
}