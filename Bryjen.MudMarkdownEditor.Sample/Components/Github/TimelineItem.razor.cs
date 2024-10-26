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
}