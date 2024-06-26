namespace AdminBackendApi;

public class LogAdmins
{
    public int ID { get; set; }
    public string? Action { get; set; }
    public string? Link { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? Content { get; set; }
    public string? UserName { get; set; }
    public bool IsDeleted { get; set; }
}