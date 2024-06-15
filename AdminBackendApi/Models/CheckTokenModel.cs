using AdminBackendApi.DataMapping;

namespace AdminBackendApi;

public class CheckTokenModel
{
    public UserAdmins? User { get; set; }
    public bool IsToken { get; set; }
}
