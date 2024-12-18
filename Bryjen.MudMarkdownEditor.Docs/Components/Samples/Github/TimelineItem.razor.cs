﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace Bryjen.MudMarkdownEditor.Docs.Components.Samples.Github;

public partial class TimelineItem : ComponentBase
{
    [Inject]
    public required IJSRuntime JsRuntime { get; set; }
    
    [Inject]
    public required IWebAssemblyHostEnvironment Environment { get; set; }
    
    [Parameter]
    public required TimelineItemModel TimelineItemModel { get; set; }
    
    [Parameter]
    public required RenderFragment AvatarRenderFragment { get; set; }
    
    [Parameter]
    public bool IsHeaderItem { get; set; } = false;

    [Parameter]
    public bool IsAuthorIssueAuthor { get; set; } = false;
    
    [Parameter]
    public bool IsAuthorRepoOwner { get; set; } = false;
}