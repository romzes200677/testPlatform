namespace CSharpTestApp.Models.api;

public class PagedResponse<T> where T : class
{
    public List<T> Items { get; set; }
    public int Count { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
}