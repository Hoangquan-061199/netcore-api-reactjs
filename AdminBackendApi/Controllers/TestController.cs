
// namespace AdminBackendApi.Controllers;

// public class TestController : BaseController
// {
//     [HttpGet]
//     public IActionResult GetCaptcha()
//     {
//         var slc = new SixLaborsCaptchaModule(new SixLaborsCaptchaOptions
//         {
//             DrawLines = 7,
//             TextColor = [Color.Blue, Color.Black],
//         });

//         var key = Extensions.GetUniqueKey(6);
//         var result = slc.Generate(key);
//         File.WriteAllBytes($"/wwwroot/Uploads", result);
//         return Ok("thanh cong");
//         // return File.WriteAllBytes($"six-labors-captcha.png", result);
//     }
//     // private readonly TestRepositories _testRepositories = new(WebConfig.ConnectionString!);
//     // [HttpGet]
//     // public IActionResult Get()
//     // {
//     //     return Ok();
//     // }
// }
