using System.Text.Json;
using Microsoft.JSInterop;

namespace Bryjen.MudMarkdownEditor.Services;

public class JsTextAreaService : IDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> m_moduleTask;
    private readonly CancellationTokenSource m_cancellationTokenSource = new();

    public JsTextAreaService(IJSRuntime jsRuntime)
    {
        m_moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", m_cancellationTokenSource.Token, "./_content/Bryjen.MudMarkdownEditor/Bryjen.MudMarkdownEditor.min.js").AsTask());
    }

    public async Task<(string Text, int StartPos, int EndPos)> GetTextAreaInfo(string textAreaId)
    {
        const string jsFunction = "getTextAreaInfo";
        
        var module = await m_moduleTask.Value;
        var jsObj = await module.InvokeAsync<dynamic>(jsFunction, textAreaId);

        if (jsObj is not JsonElement jsonElement)
        {
            throw new JSException($"Expected the return type of the js function\"{jsFunction}\" to be \"{typeof(JsonElement)}\", got \"{jsObj.GetType()}\"");
        }

        try
        {
            return (jsonElement.GetProperty("text").GetString() ?? "",
                jsonElement.GetProperty("startPos").GetInt32(),
                jsonElement.GetProperty("endPos").GetInt32());
        }
        catch (Exception exception)
        {
            throw new JSException($"\"{jsFunction}\" returned an invalid json object.", exception);
        }
    }

    public async Task ModifyTextArea(string textAreaId, string newText, int startPos, int endPos)
    {
        const string jsFunction = "modifyTextArea";
        var module = await m_moduleTask.Value;
        await module.InvokeVoidAsync(jsFunction, textAreaId, newText, startPos, endPos);
    }

    public async Task<int> GetTextAreaHeight(string textAreaId)
    {
        const string jsFunction = "getTextareaHeight";
        var module = await m_moduleTask.Value;
        var jsObj =  await module.InvokeAsync<dynamic>(jsFunction, textAreaId);
        
        if (jsObj is not JsonElement jsonElement)
        {
            throw new JSException($"Expected the return type of the js function\"{jsFunction}\" to be \"{typeof(JsonElement)}\", got \"{jsObj.GetType()}\"");
        }

        try
        {
            return jsonElement.GetInt32();
        }
        catch (Exception exception)
        {
            throw new JSException($"\"{jsFunction}\" returned an invalid json object.", exception);
        }
    }

    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        
        if (m_moduleTask.IsValueCreated)
        {
            m_moduleTask.Value.Dispose();
        }
        
        m_cancellationTokenSource.Dispose();
    }
}