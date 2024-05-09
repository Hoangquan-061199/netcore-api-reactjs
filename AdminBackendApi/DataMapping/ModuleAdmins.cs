namespace AdminBackendApi;

internal class ModuleAdmins
{
    internal int ID { get; set; }
    internal string? Name { get; set; }
    internal string? LinkUrl { get; set; }
    internal int TagId { get; set; }
    internal int ParentId { get; set; }
    internal bool IsShow { get; set; }
    internal bool IsDeleted { get; set; }
    internal string? NameAscii { get; set; }
    internal int OrderDisplay { get; set; }
}
