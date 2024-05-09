namespace AdminBackendApi;

internal class Attributes
{
    internal int ID { get; set; }
    internal string? Name { get; set; }
    internal string? NameAscii { get; set; }
    internal bool IsShow { get; set; }
    internal bool IsDeleted { get; set; }
    internal int OrderDisplay { get; set; }
    internal string? ModuleIds { get; set; }
}
