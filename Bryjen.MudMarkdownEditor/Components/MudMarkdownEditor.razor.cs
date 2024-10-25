using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;

namespace Bryjen.MudMarkdownEditor;

public partial class MudMarkdownEditor : ComponentBase
{
    [Parameter]
    public RenderFragment<(RenderFragment<string> EditorTemplate, RenderFragment<string> PreviewTemplate)>? ComponentTemplate { get; set; }

    [Parameter] 
    public RenderFragment<string>? EditorTemplate { get; set; }
    
    [Parameter]
    public RenderFragment<string>? PreviewTemplate { get; set; }


    [Parameter] 
    public string Markdown { get; set; } = string.Empty;

    [Parameter] 
    public Func<string, string> ParseMarkdownFunc { get; set; } = id => id;


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