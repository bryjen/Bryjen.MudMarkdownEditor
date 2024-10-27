using Bryjen.MudMarkdownEditor.MarkdownPipelines;
using Microsoft.AspNetCore.Components;

namespace Bryjen.MudMarkdownEditor.Components.Prebuilts;

internal enum ViewType
{
    Edit, Preview
}

public partial class MudGithubMarkdownEditor : ComponentBase
{
    private MudMarkdownEditor m_mudMarkdownEditor = null!;
    
    private ViewType m_viewType = ViewType.Edit;
}