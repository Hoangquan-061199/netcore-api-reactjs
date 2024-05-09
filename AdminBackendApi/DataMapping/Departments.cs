namespace AdminBackendApi;

public class Departments
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int OrderDisplay { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsShow { get; set; }
}
