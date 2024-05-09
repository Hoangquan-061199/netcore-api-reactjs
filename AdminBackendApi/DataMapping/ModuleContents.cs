namespace AdminBackendApi;

public class ModuleContents
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? NameAscii { get; set; }
    public string? LinkUrl { get; set; }
    public int ParentId { get; set; }
    public string? Title { get; set; }
    public bool IsShow { get; set; }
    public bool IsDeleted { get; set; }
    public int OrderDisplay { get; set; }
    public string? Lang { get; set; }
    public string? ModuleTypeCode { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public string? SEOTitle { get; set; }
    public string? SEOKeyword { get; set; }
    public string? SEODescription { get; set; }
    public string? Canonical { get; set; }
    public string? IndexGoogle { get; set; }
    public bool Sitemap { get; set; }
    public string? AttributeIds { get; set; }
    public string? UrlPicture { get; set; }
    public string? AlbumPictures { get; set; }
}
