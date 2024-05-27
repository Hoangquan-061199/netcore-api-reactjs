using Microsoft.AspNetCore.Mvc;

namespace AdminBackendApi;

public class TestController : BaseController
{

    private readonly TestRepositories _testRepositories = new(WebConfig.ConnectionString!);
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
