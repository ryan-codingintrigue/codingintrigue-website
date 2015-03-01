using System.Text;
using MarkdownDeep;

namespace BusinessLogic
{
	public class MarkdownParser : IMarkdownParser
	{
		private readonly Markdown _markdown;

		public MarkdownParser(Markdown markdown)
		{
			_markdown = markdown;
			_markdown.ExtraMode = true;
			_markdown.FormatCodeBlock = (markdownClass, s) =>
			{
				var sb = new StringBuilder();
				sb.Append("<pre class=\"prettyprint\"><code>");
				sb.Append(s);
				sb.Append("</code></pre>\n\n");
				return sb.ToString();
			};
		}

		public string GetHtmlFromMarkdown(string markdown)
		{
			return _markdown.Transform(markdown);
		}
	}
}