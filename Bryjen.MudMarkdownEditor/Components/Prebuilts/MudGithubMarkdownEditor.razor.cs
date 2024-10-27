using Bryjen.MudMarkdownEditor.MarkdownPipelines;
using Microsoft.AspNetCore.Components;

namespace Bryjen.MudMarkdownEditor.Prebuilts;

public partial class MudGithubMarkdownEditor : ComponentBase
{
    [Parameter] 
    public string Markdown { get; set; } = string.Empty;

    [Parameter] 
    public Func<string, string> ParseMarkdownFunc { get; set; } = GithubStyle.MarkdownToHtml; 
}