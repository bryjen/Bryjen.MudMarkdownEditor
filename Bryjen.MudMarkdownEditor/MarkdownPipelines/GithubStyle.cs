using HtmlAgilityPack;
using Markdig;
using Markdown.ColorCode;

namespace Bryjen.MudMarkdownEditor.MarkdownPipelines;

public class GithubStyle
{
    public static string MarkdownToHtml(string rawMarkdown)
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseColorCode(HtmlFormatterType.Style, GithubStyleDictionary.GithubStyle)
            .Build();
        
        var syntaxHighlightedHtml = global::Markdig.Markdown.ToHtml(rawMarkdown, pipeline);
        
        // Further post-processing
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(syntaxHighlightedHtml);
        
        AddScrollableCodeBlocks(ref htmlDoc);

        return $"""
                <div class="markdown-body">
                    <style>
                        {CssStyles}
                    </style>
                    {htmlDoc.DocumentNode.OuterHtml}
                </div>
                """;
    }

    private static void AddScrollableCodeBlocks(ref HtmlDocument htmlDocument)
    {
        var divNodes = htmlDocument.DocumentNode.SelectNodes("//div[not(@id) and count(*) = 1 and pre]");

        if (divNodes != null)
        {
            foreach (var div in divNodes)
            {
                div.SetAttributeValue("class", "scrollable-container");
            }
        }
    }
    
    
    private const string CssStyles = 
        """
        .markdown-body {
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", "Noto Sans", Helvetica, Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji";
            color: #d1d7e0
        }
        
        .markdown-body a {
            text-decoration: underline;
            color: #478be6; 
        }
        
        .markdown-body pre {
            font-family: monospace, ui-monospace;
            font-size: 0.75rem;
        }
        
        .markdown-body p {
            margin-bottom: 1rem;
            color: #d1d7e0;
        }
        
        .markdown-body ul {
            list-style-type: disc;
            margin-bottom: 1rem;
        }
        
        .markdown-body ul li {
            margin-left: 1.5rem;
        }
        
        .markdown-body ul li::marker {
            font-size: 1.2rem;
            unicode-bidi: isolate;
            font-variant-numeric: tabular-nums;
            text-transform: none;
            text-indent: 0px !important;
            text-align: start !important;
            text-align-last: start !important;
        }
        """;
}