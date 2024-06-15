namespace AdminBackendApi.DataMapping;

public class SystemConfigs
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Hotline { get; set; }
    public string? Facebook { get; set; }
    public string? Zalo { get; set; }
    public string? TikTok { get; set; }
    public string? Youtube { get; set; }
    public string? MailSend { get; set; }
    public string? MailTo { get; set; }
    public string? MailPass { get; set; }
    public string? MailServer { get; set; }
    public int MailPort { get; set; }
    public bool MailSSL { get; set; }
    public string? SEOTitle { get; set; }
    public string? SEOKeyword { get; set; }
    public string? SEODescription { get; set; }
    public string? Favicon { get; set; }
    public string? OgImage { get; set; }
    public string? OgImageAlt { get; set; }
    public string? IndexGoogle { get; set; }
    public bool IsShow { get; set; }
    public string? Lang { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class SystemConfigItem : BaseItem
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Hotline { get; set; }
    public string? Facebook { get; set; }
    public string? Zalo { get; set; }
    public string? TikTok { get; set; }
    public string? Youtube { get; set; }
    public string? MailSend { get; set; }
    public string? MailTo { get; set; }
    public string? MailPass { get; set; }
    public string? MailServer { get; set; }
    public int MailPort { get; set; }
    public bool MailSSL { get; set; }
    public string? SEOTitle { get; set; }
    public string? SEOKeyword { get; set; }
    public string? SEODescription { get; set; }
    public string? Favicon { get; set; }
    public string? OgImage { get; set; }
    public string? OgImageAlt { get; set; }
    public string? IndexGoogle { get; set; }
    public bool IsShow { get; set; }
    public string? Lang { get; set; }
    public DateTime CreatedDate { get; set; }
}