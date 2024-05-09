namespace AdminBackendApi;

public class ModuleAdmins
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? LinkUrl { get; set; }
    public int TagId { get; set; }
    public int ParentId { get; set; }
    public bool IsShow { get; set; }
    public bool IsDeleted { get; set; }
    public string? NameAscii { get; set; }
    public int OrderDisplay { get; set; }
}
