namespace AspNetWebApiWithMongoDb.Common;

public class PagedResultRequestDto<T>
{
    public long TotalCount { get; set; }
    public IReadOnlyList<T> Items { get; set; }
}