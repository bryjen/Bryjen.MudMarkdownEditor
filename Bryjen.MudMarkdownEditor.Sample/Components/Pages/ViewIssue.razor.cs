using TimelineItem = Bryjen.MudMarkdownEditor.Sample.Models.TimelineItem;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Bryjen.MudMarkdownEditor.Sample.Components.Pages;

public partial class ViewIssue : ComponentBase
{
    [Inject]
    public required ISnackbar Snackbar { get; set; }
    
    
    private const string RepoOwner = "bryjen";
    
    private const string IssueAuthor = "bryjen";
    
    private static Models.TimelineItem[] TimelineItemModels =>
    [
        new Models.TimelineItem
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
        
        // TODO: Remove, just temporary
        new Models.TimelineItem
        {
            ItemAuthor = "bryjen",
            PublishDate = new DateTime(2024, 10, 18, 7, 46, 12),
            Contents =
"""
```c#
var fileBytes = await m_jsRuntime.InvokeAsync<byte[]>("fetchWrs2AreasGz");

if (fileBytes == null || fileBytes.Length == 0)
{
    throw new Exception("Failed to fetch or retrieve the Gzip file.");
}

using var inputStream = new MemoryStream(fileBytes);
await using var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);   // The quick brown fox jumps over the lazy dog.
using var decompressedStream = new MemoryStream();
await gzipStream.CopyToAsync(decompressedStream);
var decompressedBytes = decompressedStream.ToArray();

using var decompressedByteMemoryStream = new MemoryStream(decompressedBytes);
return ProtoBuf.Serializer.Deserialize<Wrs2Area[]>(decompressedByteMemoryStream).ToArray();
```
""",
            IsRepoAuthor = true
        },
    ];
}