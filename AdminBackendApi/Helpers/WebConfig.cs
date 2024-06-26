namespace AdminBackendApi;

internal class WebConfig
{
    internal static string? ConnectionString { get; set; }
    private IConfiguration Configuration { get; set; }
    internal static string? PathServer { get; set; }
    internal static string? Issuer { get; set; }
    internal static string? Audience { get; set; }
    internal static string? KeyRefresh { get; set; }
    internal static string? TimeoutRefresh { get; set; }
    internal static string? TimeoutToken { get; set; }
    internal static string? KeyToken { get; set; }
    internal static bool IsLogAdmin { get; set; }
    internal static string? Sizes { get; set; }
    internal static string? PassCode { get; set; }

    internal const string UrlStartApiAdmin = "/api/admin/";
    internal WebConfig(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        Configuration = configuration;
        ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        PathServer = hostingEnvironment.WebRootPath + "/";
        Issuer = Configuration["Jwt:Issuer"];
        Audience = Configuration["Jwt:Audience"];
        KeyRefresh = Configuration["Jwt:RefeshKey"];
        TimeoutRefresh = Configuration["Jwt:ExpiresRefesh"];
        KeyToken = Configuration["Jwt:Key"];
        TimeoutToken = Configuration["Jwt:Expires"];
        Sizes = Configuration["WebConfig:Sizes"];
        IsLogAdmin = !string.IsNullOrEmpty(Configuration["WebConfig:LogErrorAdmin"]) && Convert.ToBoolean(Configuration["WebConfig:LogErrorAdmin"]);
        PassCode = Configuration["WebConfig:PassCode"];
    }
}
