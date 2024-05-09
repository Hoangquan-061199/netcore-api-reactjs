namespace AdminBackendApi;

public class DepartmentItems : BaseItems
{
    public string Name { get; set; } = string.Empty;
    public int OrderDisplay { get; set; }
    public bool IsShow { get; set; }
}
