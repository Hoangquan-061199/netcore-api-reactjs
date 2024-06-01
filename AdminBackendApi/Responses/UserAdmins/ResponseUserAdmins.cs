namespace AdminBackendApi;

public class ResponseUserAdmins
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Roles { get; set; }
    public string? RoleActive { get; set; }
    public string? ModuleAdminIds { get; set; }
    public string? ModuleWebsiteIds { get; set; }
    public bool IsShow { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public DateTime LockDate { get; set; }
    public bool IsLock { get; set; }
    public int DepartmentId { get; set; }
    public string? UrlPicture { get; set; }
}
