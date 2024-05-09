namespace AdminBackendApi;

public class ModulePosittions
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? TypeCode { get; set; }
    public string? ModuleIds { get; set; }
    public int ParentId { get; set; }
    public string? AdvertisingIds { get; set; }
    public bool IsShow { get; set; }
    public bool IsDeleted { get; set; }
    public string? Lang { get; set; }
    public int OrderDisplay { get; set; }
    public int ModuleCount { get; set; }
    public int ContentCount { get; set; }
    public int TypeView { get; set; }
    public int Sort { get; set; }
    public string? LinkUrl { get; set; }
    public string? UrlPicture { get; set; }
}
