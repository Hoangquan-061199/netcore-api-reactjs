namespace AdminBackendApi;

public class Advertisings
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string? LinkUrl { get; set; }
    public string? Content { get; set; }
    public string? ModulePositionIds { get; set; }
    public string? ModuleIds { get; set; }
    public string? UrlPicture { get; set; }
    public string? UrlPictureMobile { get; set; }
    public string? Video { get; set; }
    public int OrderDisplay { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsShow { get; set; }
    public string? Lang { get; set; }
}
