using Microsoft.AspNetCore.Components;

namespace Bryjen.MudMarkdownEditor.Sample.Models;

public class TimelineItem
{
    public string ItemAuthor { get; init; } = string.Empty;
    public DateTime PublishDate { get; init; } = DateTime.Now;
    public string Contents { get; init; } = new(string.Empty);
    public TimelineItem[] PreviousRevisions { get; set; } = [];

    public bool IsRepoAuthor { get; init; } = false;
}