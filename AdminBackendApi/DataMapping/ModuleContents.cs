namespace AdminBackendApi;

internal class ModuleContents
{
    internal int ID { get; set; }
    internal string? Name { get; set; }
    internal string? NameAscii { get; set; }
    internal string? LinkUrl { get; set; }
    internal int ParentId { get; set; }
    internal string? Title { get; set; }
    internal bool IsShow { get; set; }
    internal bool IsDeleted { get; set; }
    internal int OrderDisplay { get; set; }
    internal string? Lang { get; set; }
    internal string? ModuleTypeCode { get; set; }
    internal string? Description { get; set; }
    internal string? Content { get; set; }
    internal string? SEOTitle { get; set; }
    internal string? SEOKeyword { get; set; }
    internal string? SEODescription { get; set; }
    internal string? Canonical { get; set; }
    internal string? IndexGoogle { get; set; }
    internal bool Sitemap { get; set; }
    internal string? AttributeIds { get; set; }
    internal string? UrlPicture { get; set; }
    internal string? AlbumPictures { get; set; }
}
