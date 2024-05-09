namespace AdminBackendApi;

public class Attributes
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? NameAscii { get; set; }
    public bool IsShow { get; set; }
    public bool IsDeleted { get; set; }
    public int OrderDisplay { get; set; }
    public string? ModuleIds { get; set; }
}
