namespace AdminBackendApi;

internal class UserAdminItem
{
    internal int TotalRecord { get; set; }
    internal Guid UserId { get; set; }
    internal string? UserName { get; set; }
    internal string? FullName { get; set; }
    internal string? Email { get; set; }
    internal string? Roles { get; set; }
    internal string? RoleActive { get; set; }
    internal string? ModuleAdminIds { get; set; }
    internal string? ModuleWebsiteIds { get; set; }
    internal bool IsShow { get; set; }
    internal bool IsActive { get; set; }
    internal DateTime CreatedDate { get; set; }
    internal DateTime ModifiedDate { get; set; }
    internal DateTime LockDate { get; set; }
    internal int CountPassFail { get; set; }
    internal bool IsLock { get; set; }
    internal int DapartmentId { get; set; }
    internal string? UrlPicture { get; set; }
}
