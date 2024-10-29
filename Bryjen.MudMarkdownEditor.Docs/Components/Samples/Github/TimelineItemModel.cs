﻿namespace Bryjen.MudMarkdownEditor.Docs.Components.Samples.Github;

public class TimelineItemModel
{
    public string ItemAuthor { get; init; } = string.Empty;
    public DateTime PublishDate { get; init; } = DateTime.Now;
    public string Contents { get; init; } = new(string.Empty);
    public TimelineItemModel[] PreviousRevisions { get; set; } = [];

    public bool IsRepoAuthor { get; init; } = false;
}