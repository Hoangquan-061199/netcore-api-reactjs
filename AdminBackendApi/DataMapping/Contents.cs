namespace AdminBackendApi;

public class Contents
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? NameAscii { get; set; }
    public string? LinkUrl { get; set; }
    public string? ModuleNameAscii { get; set; }
    public string? ModuleIds { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public string? SEOTitle { get; set; }
    public string? SEOKeyword { get; set; }
    public string? SEODescription { get; set; }
    public string? Canonical { get; set; }
    public string? IndexGoogle { get; set; }
    public bool Sitemap { get; set; }
    public string? UrlPicture { get; set; }
    public string? AlbumPictures { get; set; }
    public string? Lang { get; set; }
    public bool IsShow { get; set; }
    public bool IsDeleted { get; set; }
    public int OrderDisplay { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public DateTime PublishDate { get; set; }
    public bool IsActive { get; set; }
    public string? Popular { get; set; }
}
