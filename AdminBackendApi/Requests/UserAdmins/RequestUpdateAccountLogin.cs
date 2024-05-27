namespace AdminBackendApi;

public class RequestUpdateAccountLogin
{
    public string? FullName { get; set;}
    public IFormFile? File { get; set;}
    public string? Email { get; set;}
}
