using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminBackendApi;

[Authorize]
public class ResourceController : BaseController
{
    // [HttpGet]
    // public IActionResult Get([FromQuery] string? keyword)
    // {
    //     string file = $"{WebConfig.PathServer}DataJson/Resources/ResourceVi.json";
    //     FileInfo info = new(file);
    //     if (!info.Exists) return Ok();
    //     string jsonString = Utilities.ReadFile("ResourceVi.json", "DataJson/Resources/");
    //     if (string.IsNullOrEmpty(jsonString)) return Ok();
    //     List<ResourceJson>? list = JsonConvert.DeserializeObject<List<ResourceJson>>(jsonString);
    //     if (!string.IsNullOrEmpty(keyword))
    //     {
    //         if (list != null && list.Count > 0)
    //             list = list.Where(x => x.Code!.Equals(keyword, StringComparison.CurrentCultureIgnoreCase) || x.Value!.Equals(keyword, StringComparison.CurrentCultureIgnoreCase)).ToList();
    //     }
    //     return Ok(list);
    // }

    // [HttpPost]
    // public IActionResult Create(RequestResourceJson req)
    // {
    //     MessagesModel msg = new()
    //     {
    //         Message = "Tạo Resource thất bại :)"
    //     };

    //     try
    //     {
    //         string id = Utilities.GenerateUniqueId();
    //         string folder = $"{WebConfig.PathServer}DataJson/Resources/";
    //         if (!Directory.Exists(folder))
    //         {
    //             Directory.CreateDirectory(folder);
    //         }
    //         string file = $"{WebConfig.PathServer}DataJson/Resources/ResourceVi.json";
    //         FileInfo info = new(file);
    //         if (info.Exists)
    //         {
    //             string jsonString = Utilities.ReadFile("ResourceVi.json", "DataJson/Resources/");
    //             int DeleteStatus = Utilities.DeleteFile("ResourceVi.json", "DataJson/Resources/");
    //             if (DeleteStatus == 0) return BadRequest(msg);
    //             if (!string.IsNullOrEmpty(jsonString))
    //             {
    //                 List<ResourceJson>? list = JsonConvert.DeserializeObject<List<ResourceJson>>(jsonString);
    //                 if (list != null && list.Count > 0)
    //                 {
    //                     ResourceJson json = new()
    //                     {
    //                         Id = id,
    //                         Code = req.Code,
    //                         Value = req.Value
    //                     };
    //                     list.Add(json);
    //                     string jsonString2 = JsonConvert.SerializeObject(list);
    //                     Utilities.CreateAppendFile("ResourceVi.json", jsonString2, "DataJson/Resources/");
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             List<ResourceJson> list = [];
    //             ResourceJson json = new()
    //             {
    //                 Id = id,
    //                 Code = req.Code,
    //                 Value = req.Value
    //             };
    //             list.Add(json);
    //             string jsonString = JsonConvert.SerializeObject(list);
    //             Utilities.CreateAppendFile("ResourceVi.json", jsonString, "DataJson/Resources/");
    //         }
    //         msg.Message = "Tạo mới thành công <3";
    //         return Ok(msg);
    //     }
    //     catch (Exception e)
    //     {
    //         Utilities.AddLogError(e);
    //         return NotFound(msg);
    //     }
    // }

    // [HttpPut]
    // public IActionResult Update(ResourceJson req)
    // {
    //     MessagesModel msg = new()
    //     {
    //         Message = "Cập nhật Resource thất bại :)"
    //     };
    //     try
    //     {
    //         string file = $"{WebConfig.PathServer}DataJson/Resources/ResourceVi.json";
    //         FileInfo info = new(file);
    //         if (!info.Exists) return BadRequest(msg);
    //         string jsonString = Utilities.ReadFile("ResourceVi.json", "DataJson/Resources/");
    //         if (string.IsNullOrEmpty(jsonString)) return BadRequest(msg);
    //         List<ResourceJson>? list = JsonConvert.DeserializeObject<List<ResourceJson>>(jsonString);
    //         if (list == null || list.Count == 0) return BadRequest(msg);
    //         if (!list.Any(x => x.Id == req.Id)) return BadRequest(msg);
    //         list = list.Where(x => x.Id != req.Id).ToList();
    //         list.Add(req);
    //         int DeleteStatus = Utilities.DeleteFile("ResourceVi.json", "DataJson/Resources/");
    //         if (DeleteStatus == 0) return BadRequest(msg);
    //         string jsonString2 = JsonConvert.SerializeObject(list);
    //         Utilities.CreateAppendFile("ResourceVi.json", jsonString2, "DataJson/Resources/");

    //         msg.Message = "Cập nhật thành công <3";
    //         return Ok(msg);
    //     }
    //     catch (Exception e)
    //     {
    //         Utilities.AddLogError(e);
    //         return NotFound(msg);
    //     }

    // }
}
