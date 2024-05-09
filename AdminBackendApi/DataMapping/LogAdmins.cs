namespace AdminBackendApi;

public class LogAdmins
{
    public int ID { get; set; }
    public string? Action { get; set; }
    public string? Url { get; set; }
    public Guid UserIDValue { get; set; }
    public DateTime DateCreated { get; set; }
    public string? Content { get; set; }
    public string? UserLogin { get; set; }
    public string? ClassControl { get; set; }
}
