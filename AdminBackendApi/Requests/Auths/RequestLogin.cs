namespace AdminBackendApi.Requests.Auths;

public class RequestLogin
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
