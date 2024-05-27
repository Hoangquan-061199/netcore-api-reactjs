namespace AdminBackendApi;

public class RequestChangePassword
{
    public string? PasswordOld { get; set; }
    public string? PasswordNew { get; set; }
    public string? PasswordConfirm { get; set; }
}
