using System;

namespace BusinessLogic
{
    public interface IMarkdownParser
    {
		string GetHtmlFromMarkdown(string markdown);
    }
}