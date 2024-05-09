namespace AdminBackendApi;

internal class ModulePosittions
{
    internal int ID { get; set; }
    internal string? Name { get; set; }
    internal string? Code { get; set; }
    internal string? TypeCode { get; set; }
    internal string? ModuleIds { get; set; }
    internal int ParentId { get; set; }
    internal string? AdvertisingIds { get; set; }
    internal bool IsShow { get; set; }
    internal bool IsDeleted { get; set; }
    internal string? Lang { get; set; }
    internal int OrderDisplay { get; set; }
    internal int ModuleCount { get; set; }
    internal int ContentCount { get; set; }
    internal int TypeView { get; set; }
    internal int Sort { get; set; }
    internal string? LinkUrl { get; set; }
    internal string? UrlPicture { get; set; }
}
