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
        
        // We have to modify the html directly. Due to the implementation of 'Markdown.ColorCode', we can't modify the 
        // markdown pipeline to change the elements included with the 'div' because the required classes are internal.
        
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(syntaxHighlightedHtml);
        
        ModifyCodeBlocks(ref htmlDoc);

        return $"""
                <div class="markdown-body">
                    <style>
                        {CssStyles}
                    </style>
                    {htmlDoc.DocumentNode.OuterHtml}
                </div>
                """;
    }

    private static void ModifyCodeBlocks(ref HtmlDocument htmlDocument)
    {
        var divNodes = htmlDocument.DocumentNode.SelectNodes("//div[not(@id) and count(*) = 1 and pre]");

        if (divNodes != null)
        {
            int currentCodeBlock = 0;
            
            foreach (var div in divNodes)
            {
                if (div is not null)
                {
                    string baseId = $"code-block-{++currentCodeBlock}";
                    
                    div.SetAttributeValue("id", baseId);
                    div.SetAttributeValue("class", "scrollable-container");
                    div.AppendChild(HtmlNode.CreateNode(ClipboardButton($"{baseId}-clipboard-button")));
                }
            }
        }
    }

    private static string ClipboardButton(string id) =>
        $"""
        <div id="{id}" class="copy-to-clipboard-container">
            <svg aria-hidden="true" height="16" viewBox="0 0 16 16" version="1.1" width="16" data-view-component="true" style="position: absolute; top: 25%; right: 25%; color: white; filter: brightness(0) invert(0.4);">
                <path d="M0 6.75C0 5.784.784 5 1.75 5h1.5a.75.75 0 0 1 0 1.5h-1.5a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-1.5a.75.75 0 0 1 1.5 0v1.5A1.75 1.75 0 0 1 9.25 16h-7.5A1.75 1.75 0 0 1 0 14.25Z"></path><path d="M5 1.75C5 .784 5.784 0 6.75 0h7.5C15.216 0 16 .784 16 1.75v7.5A1.75 1.75 0 0 1 14.25 11h-7.5A1.75 1.75 0 0 1 5 9.25Zm1.75-.25a.25.25 0 0 0-.25.25v7.5c0 .138.112.25.25.25h7.5a.25.25 0 0 0 .25-.25v-7.5a.25.25 0 0 0-.25-.25Z"></path>
            </svg>
        </div>
        """;
    
    
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
        
        .markdown-body ul li.task-list-item::marker {
        }
        
        
        .scrollable-container {
            position: relative;
        
            width: 100%;
            white-space: pre;
            overflow-x: auto;
            box-sizing: border-box;
            
            border-radius: 0.25rem;
            
            padding: 1rem;
            background-color: #262c36;
        }
        
        
        .copy-to-clipboard-container {
            position: absolute; 
            top: 0; 
            right: 0; 
            
            width: 2rem; 
            height: 2rem; 
            margin: 0.5rem;
            
            background-color: #2a313c; 
            border-radius: 0.4rem;
            outline: 1px solid #3d444d;
            
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }
        
        .copy-to-clipboard-container:hover {
            background-color: #2f3742; 
        }
        
        .copy-to-clipboard-container:active {
            outline: 1px solid #337339; 
        }
        """;
}