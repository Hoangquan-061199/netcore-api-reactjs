namespace AdminBackendApi;

public class UploadFileModel
{
    public string? Message { get; set;}
    public bool IsError { get; set; } = false;
    public string? UrlPicture { get; set; }
}
