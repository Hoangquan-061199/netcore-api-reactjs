namespace AdminBackendApi;

public class Members
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? PasswordSalt { get; set; }
    public int Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? FullName { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsLock { get; set; }
    public string? Code { get; set; }
    public DateTime CodeExpired { get; set; }
    public DateTime LockTime { get; set; }
    public DateTime CreatedDate { get; set; }
}
