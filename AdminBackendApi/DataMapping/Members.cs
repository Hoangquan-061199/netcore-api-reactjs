namespace AdminBackendApi;

internal class Members
{
    internal Guid UserId { get; set; }
    internal string? UserName { get; set; }
    internal string? Password { get; set; }
    internal string? PasswordSalt { get; set; }
    internal int Phone { get; set; }
    internal string? Email { get; set; }
    internal string? Address { get; set; }
    internal string? FullName { get; set; }
    internal bool IsActive { get; set; }
    internal bool IsDeleted { get; set; }
    internal bool IsLock { get; set; }
    internal string? Code { get; set; }
    internal DateTime CodeExpired { get; set; }
    internal DateTime LockTime { get; set; }
    internal DateTime CreatedDate { get; set; }
}
