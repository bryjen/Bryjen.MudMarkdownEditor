using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Bryjen.MudMarkdownEditor.Sample.MarkdownPipelines;

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


    private string m_rawMarkdown = String.Empty;
    private string m_processedHtml = string.Empty;

    
    protected override void OnParametersSet()
    {
        var rawMarkdown = TimelineItemModel.Contents.Trim();
        m_rawMarkdown = rawMarkdown;
        m_processedHtml = MarkdigColorCode.MarkdownToHtml(rawMarkdown);
    }
}