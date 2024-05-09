namespace AdminBackendApi;

internal class LogAdmins
{
    internal int ID { get; set; }
    internal string? Action { get; set; }
    internal string? Url { get; set; }
    internal Guid UserIDValue { get; set; }
    internal DateTime DateCreated { get; set; }
    internal string? Content { get; set; }
    internal string? UserLogin { get; set; }
    internal string? ClassControl { get; set; }
}
