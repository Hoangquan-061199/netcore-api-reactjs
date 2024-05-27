namespace AdminBackendApi;

public class ResponseLogin
{
    public string? Token { get; set; }
    public string? Message { get; set; }
    public bool Error { get; set; } = true;
}
