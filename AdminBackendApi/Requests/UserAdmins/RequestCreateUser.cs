namespace AdminBackendApi;

public class RequestCreateUser
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Roles { get; set; }
    public int DepartmentId { get; set; }
    public string? UrlPicture { get; set; }
}
