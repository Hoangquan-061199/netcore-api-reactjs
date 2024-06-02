using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBackendApi;

[Authorize]
public class LogAdminController : BaseController
{
    private readonly LogAdminRepositories _logAdminRepositories = new(WebConfig.ConnectionString!);

    /// <summary>
    /// Lấy ra tất cả bản ghi log 
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetLog()
    {
        MessagesModel msg = new()
        {
            Message = "Tải thất bại :)"
        };
        try
        {
            List<LogAdmins>? list = await _logAdminRepositories.GetAll();
            return Ok(list);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Xoá bản ghi theo id
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult> Detele(int id)
    {
        MessagesModel msg = new()
        {
            Message = "Xoá thất bại :)"
        };
        try
        {
            int rs = await _logAdminRepositories.Delete(id);
            if (rs == 0) throw new Exception(msg.Message);
            msg.Message = "Xoá thành công <3";
            return Ok(msg);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

}
