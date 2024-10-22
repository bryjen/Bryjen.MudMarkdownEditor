﻿using Bryjen.MudMarkdownEditor.Sample.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Bryjen.MudMarkdownEditor.Sample.Components.Pages;

public partial class ViewIssue : ComponentBase
{
    [Inject]
    public required ISnackbar Snackbar { get; set; }
    
    [Inject]
    public required IJSRuntime JsRuntime { get; set; }

    
    protected override async Task OnAfterRenderAsync(bool isFirstRender)
    {
        if (isFirstRender)
        {
            /*
            var module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./dist/index.js");
            await module.InvokeVoidAsync("myStarryNightWrapper.loadStarryNight");
            */
            
            await JsRuntime.InvokeVoidAsync("myFunction");
        }
    }
    

    private const string RepoOwner = "bryjen";
    
    private const string IssueAuthor = "bryjen";
    
    private static TimelineItem[] TimelineItemModels =>
    [
        new TimelineItem
        {
            ItemAuthor = "bryjen",
            PublishDate = new DateTime(2024, 10, 17, 23, 14, 34),
            Contents = 
"""
As of right now, we're facing two problems regarding WRS2 areas:

- [ ] Speed up the processing the file
- [ ] Speed up fetching the path & row scenes that contain a given point

As of the making of this ticket, this is the current state of the application, displaying both of the above:
https://github.com/user-attachments/assets/122aa9c7-e7a7-4231-bf6f-a15e3e008e97
""",
            IsRepoAuthor = true
        },
        new TimelineItem
        {
            ItemAuthor = "bryjen",
            PublishDate = new DateTime(2024, 10, 17, 23, 46, 9),
            Contents = 
"""
Below is the code for loading the wrs2 areas information into memory. It can be split into two processes: reading the bytes, and deserializing the data. The first part does not take that long, and there looks to be no need to further optimize it. However, deserializing the data takes significantly longer and is the cause for the second loading that occurs when blazor is loaded. I've tried several different ways to try and make it so that it doesn't block the UI, but I haven't found a way to do it so far.

The initial approach I can take is see if theres any way to configure protobuf and/or the model classes to speed up deserialization. A solution that slightly increases the size of the file but significantly increases the deserialization time would be suitable.

Below is the code for the processing of the file (there is a separate, very simple js function that reads the bytes):
```c#
var fileBytes = await m_jsRuntime.InvokeAsync<byte[]>("fetchWrs2AreasGz");

if (fileBytes == null || fileBytes.Length == 0)
{
    throw new Exception("Failed to fetch or retrieve the Gzip file.");
}

using var inputStream = new MemoryStream(fileBytes);
await using var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);
using var decompressedStream = new MemoryStream();
await gzipStream.CopyToAsync(decompressedStream);
var decompressedBytes = decompressedStream.ToArray();

using var decompressedByteMemoryStream = new MemoryStream(decompressedBytes);
return ProtoBuf.Serializer.Deserialize<Wrs2Area[]>(decompressedByteMemoryStream).ToArray();
```
""",
            IsRepoAuthor = true
        },
        new TimelineItem
        {
            ItemAuthor = "bryjen",
            PublishDate = new DateTime(2024, 10, 18, 7, 46, 12),
            Contents =
"""
As for fetching path & row scenes from the given lat and long coordinate, it seems that mainly the problem comes from iterating through all stored areas (which is >25000 in length). To clarify on what we're trying to do, we're trying to:

- Minimize the number of iterations as much as possible
- Minimize the time it takes to process whether a point is contained in a polygon (defined by a list of coordinates)

One possible approach that i'm looking into right now is to group the areas up into larger areas. For example, if the coordinate is in a certain hemisphere, only iterate through the areas that are in that hemisphere. Following up on that same logic, we can futher divide a hemisphere into sub hemispheres, and only iterate through the areas that are located in the sub hemisphere that the coordinate is contained in.
""",
            IsRepoAuthor = true
        },
    ];
}