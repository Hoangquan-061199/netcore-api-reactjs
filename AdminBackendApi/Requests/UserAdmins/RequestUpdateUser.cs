namespace AdminBackendApi;

public class RequestUpdateUser
{
    public string? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Roles { get; set; }
    public int DepartmentId { get; set; }
    public string? UrlPicture { get; set; }
}
