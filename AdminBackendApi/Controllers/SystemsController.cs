using AdminBackendApi.DataMapping;
using AdminBackendApi.Models;
using AdminBackendApi.Repositories;
using AdminBackendApi.Requests.SystemConfigs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBackendApi.Controllers;

[Authorize]
public class SystemsController : BaseController
{

    private readonly SystemsRepositories _systemsRepositories = new(WebConfig.ConnectionString!);

    readonly MessagesModel msg = new();

    /// <summary>
    /// Lấy ra danh sách user
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUserAll([FromQuery] SearchModel search)
    {
        try
        {
            List<SystemConfigItem>? ListSearchs = await _systemsRepositories.GetListSearch(search);
            return Ok(ListSearchs);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Tạo mới systems
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(RequestCreateSystemConfig obj)
    {
        msg.Message = "Tạo mới cấu hình thất bại :)";
        try
        {
            SystemConfigs system = new();
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    var val = obj.GetType().GetProperty(prop.Name)!.GetValue(obj, null);
                    if (val != null)
                    {
                        val = Utilities.RemoveHTMLTag(val.ToString()!);
                    }
                }
            }

            #region check Lang
            int checkLang = await _systemsRepositories.CheckLang(obj.Lang!);
            if (checkLang > 0)
            {
                msg.Message = "Cấu hình này đã tồn tại ở ngôn ngữ " + obj.Lang;
                throw new Exception(msg.Message);
            }
            #endregion

            system.Name = obj.Name;
            system.Address = obj.Name;
            system.Email = obj.Email;
            system.Hotline = obj.Hotline;
            system.Facebook = obj.Facebook;
            system.Zalo = obj.Zalo;
            system.TikTok = obj.TikTok;
            system.Youtube = obj.Youtube;
            system.MailSend = obj.MailSend;
            system.MailTo = obj.MailTo;
            system.MailPass = obj.MailPass;
            system.MailServer = obj.MailServer;
            system.MailPort = obj.MailPort;
            system.MailSSL = obj.MailSSL;
            system.SEOTitle = obj.SEOTitle;
            system.SEOKeyword = obj.SEOKeyword;
            system.SEODescription = obj.SEODescription;
            system.Favicon = obj.Favicon;
            system.OgImage = obj.OgImage;
            system.OgImageAlt = obj.OgImageAlt;
            system.IndexGoogle = obj.IndexGoogle;
            system.IsShow = obj.IsShow;
            system.Lang = obj.Lang;
            system.IsDeleted = false;
            system.CreatedDate = DateTime.Now;

            int rs = await _systemsRepositories.Insert(system);
            if (rs == 0) throw new Exception(msg.Message);
            msg.Message = "Tạo mới cấu hình thành công :3";
            AddLogAdmin("/api/admin/systemconfig", "Tạo mới cấu hình " + system.Name, "Create-SystemConfig");
            return Ok(msg);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// cập nhật systems
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update(ResquestUpdateSystemConfig obj)
    {
        msg.Message = "Cập nhật cấu hình thất bại :)";
        try
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    var val = obj.GetType().GetProperty(prop.Name)!.GetValue(obj, null);
                    if (val != null)
                    {
                        val = Utilities.RemoveHTMLTag(val.ToString()!);
                    }
                }
            }

            SystemConfigs? system = await _systemsRepositories.GetById(obj.ID) ?? throw new Exception(msg.Message);

            #region check Lang
            int checkLang = await _systemsRepositories.CheckLang(obj.Lang!);
            if (checkLang > 0 && checkLang != system.ID)
            {
                msg.Message = "Cấu hình này đã tồn tại ở ngôn ngữ " + obj.Lang;
                throw new Exception(msg.Message);
            }
            #endregion

            int rs = await _systemsRepositories.Update(obj, "SystemConfigs");
            if (rs == 0) throw new Exception(msg.Message);
            msg.Message = "Cập nhật cấu hình thành công :3";
            AddLogAdmin("/api/admin/systemconfig", "Cập nhật cấu hình " + system.Name, "Update-SystemConfig");
            return Ok(msg);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Xoá systems
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        msg.Message = "Cập nhật cấu hình thất bại :)";
        try
        {
            SystemConfigs? system = await _systemsRepositories.GetById(id) ?? throw new Exception(msg.Message);

            int rs = await _systemsRepositories.Delete(new
            {
                ID = id
            }, "SystemConfigs");
            if (rs == 0) throw new Exception(msg.Message);
            msg.Message = "Xoá cấu hình thành công :3";
            AddLogAdmin("/api/admin/systemconfig", "Xoá cấu hình " + system.Name, "Deleted-SystemConfig");
            return Ok(msg);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }



}
