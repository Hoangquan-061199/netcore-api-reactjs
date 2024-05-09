namespace AdminBackendApi;

public class SearchModel
{
    public string? Keyword { get; set; }
    public int Sort { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
