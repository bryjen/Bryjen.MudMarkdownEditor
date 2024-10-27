using Bryjen.MudMarkdownEditor.MarkdownPipelines;
using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;

namespace Bryjen.MudMarkdownEditor;

public partial class MudMarkdownEditor : ComponentBase
{
    [Parameter]
    public RenderFragment<(RenderFragment EditorTemplate, RenderFragment PreviewTemplate)> ComponentTemplate { get; set; } = null!;

    [Parameter] 
    public RenderFragment EditorTemplate { get; set; } = null!;

    [Parameter] 
    public RenderFragment PreviewTemplate { get; set; } = null!;


    [Parameter] 
    public string Markdown { get; set; } = string.Empty;

    [Parameter] 
    public Func<string, string> ParseMarkdownFunc { get; set; } = GithubStyle.MarkdownToHtml; 


    [Parameter] 
    public string Class { get; set; } = string.Empty;
    
    [Parameter] 
    public string Style { get; set; } = string.Empty;
    
    
    private string Classes =>
        new CssBuilder()
            .AddClass(Class)
            .Build();
    
    private string Styles =>
        new StyleBuilder()
            .AddStyle(Style)
            .AddStyle("display: flex")
            .AddStyle("align-items: center")
            .AddStyle("justify-content: space-between")
            .AddStyle("flex-direction: column")
            .Build();
    
}