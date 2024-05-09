namespace AdminBackendApi;

internal class Advertisings
{
    internal int ID { get; set; }
    internal string? Name { get; set; }
    internal string? Title { get; set; }
    internal string? LinkUrl { get; set; }
    internal string? Content { get; set; }
    internal string? ModulePositionIds { get; set; }
    internal string? ModuleIds { get; set; }
    internal string? UrlPicture { get; set; }
    internal string? UrlPictureMobile { get; set; }
    internal string? Video { get; set; }
    internal int OrderDisplay { get; set; }
    internal bool IsDeleted { get; set; }
    internal bool IsShow { get; set; }
    internal string? Lang { get; set; }
}
