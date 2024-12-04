using System.Text.RegularExpressions;
using Bryjen.MudMarkdownEditor.MarkdownPipelines;
using Bryjen.MudMarkdownEditor.Services;
using Markdig.Helpers;
using Microsoft.AspNetCore.Components;

namespace Bryjen.MudMarkdownEditor.Components.Prebuilts;

internal enum ViewType
{
    Edit, Preview
}

public partial class MudGithubMarkdownEditor : ComponentBase
{
    [Inject]
    public required JsTextAreaService JsTextAreaService { get; set; }
    
    
    private MudMarkdownEditor m_mudMarkdownEditor = null!;
    
    private ViewType m_viewType = ViewType.Edit;

    private Guid m_textAreaId = Guid.NewGuid();

    private int m_textAreaHeightPx = 100;


    private async Task SwitchView()
    {
        switch (m_viewType)
        {
            case ViewType.Edit:
                m_textAreaHeightPx = await JsTextAreaService.GetTextAreaHeight(m_textAreaId.ToString());
                m_viewType = ViewType.Preview;
                break;
            case ViewType.Preview:
                // height was set manually in the 'style' attribute of the textarea
                m_viewType = ViewType.Edit;
                break;
        }
    }

    
    protected virtual async Task HeaderButtonPressed()
    {
        var (text, startPos, _) = await JsTextAreaService.GetTextAreaInfo(m_textAreaId.ToString());
        
        while (startPos > 0 && !text[startPos - 1].IsWhiteSpaceOrZero())
        {
            startPos--;
        }

        string newText = $"{text[..startPos]} ### {text[startPos..]}";
        m_mudMarkdownEditor.SetMarkdown(newText);
        await JsTextAreaService.ModifyTextArea(m_textAreaId.ToString(), newText, startPos + 5, startPos + 5);
    }

    protected virtual async Task BoldButtonPressed()
    {
        FormatSlicesFunc formatSlicesFunc = 
            (beforeSelection, selection, afterSelection) => $"{beforeSelection}**{selection}**{afterSelection}";
        
        await FormatTextAreaSelection(
            formatSlicesFunc, 
            positions => positions.StartPos + 2, 
            positions => positions.EndPos + 2);
    }
    
    protected virtual async Task ItalicButtonPressed()
    {
        FormatSlicesFunc formatSlicesFunc = 
            (beforeSelection, selection, afterSelection) => $"{beforeSelection}_{selection}_{afterSelection}";
        
        await FormatTextAreaSelection(
            formatSlicesFunc, 
            positions => positions.StartPos + 1, 
            positions => positions.EndPos + 1);
    }
    
    protected virtual async Task QuoteButtonPressed()
    {
        var (text, startPos, endPos) = await JsTextAreaService.GetTextAreaInfo(m_textAreaId.ToString());
        string newText;
        
        if (startPos != endPos)
        {
            // In the case of a selection, split before & after selection
            AdjustPositionsForSelection(text, ref startPos, ref endPos);
            newText = $"{text[..startPos]}\n\n> {text[startPos..endPos]}\n\n{text[endPos..]}";
            m_mudMarkdownEditor.SetMarkdown(newText);
            await JsTextAreaService.ModifyTextArea(m_textAreaId.ToString(), newText, startPos + 4, endPos + 4);
        }
        else
        {
            // In the case without a selection, seeks the closest whitespace to the left, then split
            while (startPos > 0 && !text[startPos - 1].IsWhiteSpaceOrZero())
            {
                startPos--;
            }

            newText = $"{text[..startPos]}\n\n> {text[startPos..]}";
            m_mudMarkdownEditor.SetMarkdown(newText);
            await JsTextAreaService.ModifyTextArea(m_textAreaId.ToString(), newText, startPos + 4, startPos + 4);
        }
    }
    
    protected virtual async Task CodeButtonPressed()
    {
        FormatSlicesFunc formatSlicesFunc = 
            (beforeSelection, selection, afterSelection) => $"{beforeSelection}`{selection}`{afterSelection}";
        
        await FormatTextAreaSelection(
            formatSlicesFunc, 
            positions => positions.StartPos + 1, 
            positions => positions.EndPos + 1);
    }
    
    protected virtual async Task LinkButtonPressed()
    {
        FormatSlicesFunc formatSlicesFunc = 
            (beforeSelection, selection, afterSelection) => $"{beforeSelection}[{selection}](url){afterSelection}";
        
        await FormatTextAreaSelection(
            formatSlicesFunc, 
            positions => positions.EndPos + 3, 
            positions => positions.EndPos + 6);
    }

    protected virtual async Task NumberedListButtonPressed()
    {
        await FormatTextAreaLine(
            formatSlice: slice => $"1. {slice}", 
            newCursorPosFunc: startPos => startPos + 3,
            regexPattern: @"^\d+. ");
    }
    
    protected virtual async Task UnorderedListButtonPressed()
    {
        await FormatTextAreaLine(
            formatSlice: slice => $"- {slice}", 
            newCursorPosFunc: startPos => startPos + 2,
            regexPattern: "^- ");
    }
    
    protected virtual async Task TaskListButtonPressed()
    {
        await FormatTextAreaLine(
            formatSlice: slice => $"- [ ] {slice}", 
            newCursorPosFunc: startPos => startPos + 6,
            regexPattern: @"^- \[[ x]\]");
    }
    
    
    
    private delegate string FormatSlicesFunc(string beforeSelection, string selection, string afterSelection);

    private async Task FormatTextAreaSelection(
        FormatSlicesFunc formatSlicesFunc, 
        Func<(int StartPos, int EndPos), int> newSelectionStartPosFunc, 
        Func<(int StartPos, int EndPos), int> newSelectionEndPosFunc)
    {
        var (text, startPos, endPos) = await JsTextAreaService.GetTextAreaInfo(m_textAreaId.ToString());
        
        if (startPos != endPos)
        {
            AdjustPositionsForSelection(text, ref startPos, ref endPos);
        }
        else
        {
            TrySelectAdjacentWord(text, ref startPos, ref endPos);
        }
        
        string newText = formatSlicesFunc(text[..startPos], text[startPos..endPos], text[endPos..]);
        m_mudMarkdownEditor.SetMarkdown(newText);
        await JsTextAreaService.ModifyTextArea(m_textAreaId.ToString(), 
            newText,
            newSelectionStartPosFunc((startPos, endPos)),
            newSelectionEndPosFunc((startPos, endPos)));
    }

    private async Task FormatTextAreaLine(Func<string, string> formatSlice, Func<int, int> newCursorPosFunc,
        string regexPattern)
    {
        var (text, startPos, _) = await JsTextAreaService.GetTextAreaInfo(m_textAreaId.ToString());
        
        while (startPos > 0 && !text[startPos - 1].IsNewLineOrLineFeed())
        {
            startPos--;
        }

        string modifiedText;
        int cursorPosition;
        if (Regex.IsMatch(text[startPos..], regexPattern))  // if bullet point, we remove it
        {
            modifiedText = Regex.Replace(text[startPos..], regexPattern, "");
            cursorPosition = startPos;
        }
        else
        {
            modifiedText = formatSlice(text[startPos..]);
            cursorPosition = newCursorPosFunc(startPos);
        }

        string newText = $"{text[..startPos]}{modifiedText}";
        m_mudMarkdownEditor.SetMarkdown(newText);
        await JsTextAreaService.ModifyTextArea(m_textAreaId.ToString(), newText, cursorPosition, cursorPosition);
    }

    // Adjusts the starting and ending position pointers to not include any spaces. 
    // Additionally, it ensures that string splicing works.
    private static void AdjustPositionsForSelection(string text, ref int startPos, ref int endPos)
    {
        while (text[startPos].IsWhiteSpaceOrZero())
        {
            startPos++;
        }

        endPos--;
        while (text[endPos].IsWhiteSpaceOrZero())
        {
            endPos--;
        }
        endPos++;
    }

    // In the case that there is no selection, we try to select the adjacent word to the cursor.
    private static void TrySelectAdjacentWord(string text, ref int startPos, ref int endPos)
    {
        while (startPos > 0 && !text[startPos - 1].IsWhiteSpaceOrZero())
        {
            startPos--;
        }

        while (endPos < text.Length && !text[endPos].IsWhiteSpaceOrZero())
        {
            endPos++;
        }
    }
}
