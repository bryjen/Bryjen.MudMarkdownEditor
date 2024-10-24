using Bryjen.MudMarkdownEditor.Sample.Markdown.Markdig;
using ColorCode.Styling;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.SyntaxHighlighting;
using Markdown.ColorCode;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Bryjen.MudMarkdownEditor.Sample.Components.Github;

public partial class TimelineItem : ComponentBase
{
    [Inject]
    public required IJSRuntime JsRuntime { get; set; }
    
    [Inject]
    public required IWebHostEnvironment Environment { get; set; }
    
    [Parameter]
    public required Models.TimelineItem TimelineItemModel { get; set; }
    
    [Parameter]
    public required RenderFragment AvatarRenderFragment { get; set; }
    
    [Parameter]
    public bool IsHeaderItem { get; set; } = false;

    [Parameter]
    public bool IsAuthorIssueAuthor { get; set; } = false;
    
    [Parameter]
    public bool IsAuthorRepoOwner { get; set; } = false;


    private string m_styles = string.Empty;
    private string m_rawHtml = String.Empty;
    private string m_processedHtml = string.Empty;
    
    
    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            var cssStyles = await File.ReadAllTextAsync("wwwroot/starry-night/css/tritanopia-dark.css");
            m_styles = cssStyles.Replace(".", "comment-markdown.");
            
            
            MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();

            var renderer = new HtmlRenderer(new StringWriter());
            renderer.ObjectRenderers.Replace<CodeBlockRenderer>(new StarryNightCodeBlockRenderer());

            var rawMarkdown = TimelineItemModel.Contents;
            m_rawHtml = Markdig.Markdown.ToHtml(rawMarkdown, pipeline);
            m_processedHtml = await JsRuntime.InvokeAsync<string>("parseMarkdownWithIt", rawMarkdown);
            // m_processedHtml = Markdig.Markdown.ToHtml(rawMarkdown);
            await JsRuntime.InvokeVoidAsync("console.log", m_processedHtml);
            
            StateHasChanged();
        }
    }
}