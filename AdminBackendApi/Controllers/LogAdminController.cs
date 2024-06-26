﻿using AdminBackendApi.Models;
using AdminBackendApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBackendApi.Controllers;

[Authorize]
public class LogAdminController : BaseController
{
    private readonly LogAdminRepositories _logAdminRepositories = new(WebConfig.ConnectionString!);
    readonly ResponseModel msg = new()
    {
        Message = "Tải thất bại :)"
    };
    /// <summary>
    /// Lấy ra tất cả bản ghi log 
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> GetLog()
    {
        try
        {
            SearchModel search = new();
            await TryUpdateModelAsync(search);
            List<LogAdmins>? list = await _logAdminRepositories.GetListSearch(search);
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
        msg.Message = "Xoá thất bại :)";
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
