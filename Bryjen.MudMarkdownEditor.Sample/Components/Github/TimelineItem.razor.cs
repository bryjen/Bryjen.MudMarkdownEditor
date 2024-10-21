using Microsoft.AspNetCore.Components;

namespace Bryjen.MudMarkdownEditor.Sample.Components.Github;

public partial class TimelineItem : ComponentBase
{
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