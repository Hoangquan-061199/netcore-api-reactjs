﻿namespace AdminBackendApi;

public class TagModuleAdmins
{
    public int ID { get; set; }
    public string? ModuleAdminId { get; set; }
    public string? Name { get; set; }
    public int OrderDisplay { get; set; }
    public bool IsShow { get; set; }
    public bool IsDeleted { get; set; }
}
