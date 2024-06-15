using AdminBackendApi.DataMapping;
using AdminBackendApi.Models;
using AdminBackendApi.Repositories;
using AdminBackendApi.Requests.UserAdmins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBackendApi.Controllers;
[Authorize]
public class UserController : BaseController
{
    private readonly UserRepositories _userRepositories = new(WebConfig.ConnectionString!);

    readonly MessagesModel msg = new()
    {
        Message = "Tải thất bại :)"
    };
    /// <summary>
    /// Lấy ra danh sách user
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUserAll([FromQuery] SearchModel search)
    {
        try
        {
            List<UserAdminItem>? ListSearchs = await _userRepositories.GetListUserBySearch(search);
            return Ok(ListSearchs);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Lấy ra user theo token
    /// </summary>
    [HttpGet("GetUserHeader")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            string? userId = GetUserAdminByToken(token);
            if (string.IsNullOrEmpty(userId)) throw new Exception(msg.Message);
            UserAdmins? user = await _userRepositories.GetUserHeaderByUserId(userId) ?? throw new Exception(msg.Message);
            ResponseUserHeader obj = new()
            {
                FullName = user.FullName,
                UrlPicture = user.UrlPicture,
                Roles = user.Roles,
                UserId = userId
            };
            return Ok(obj);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Lấy ra user theo token
    /// </summary>
    [HttpGet("GetUserUpdate")]
    public async Task<IActionResult> GetUserUpdate()
    {

        try
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            string? userId = GetUserAdminByToken(token);
            if (string.IsNullOrEmpty(userId)) throw new Exception(msg.Message);
            UserAdmins? user = await _userRepositories.GetUserUpdateByUserId(userId) ?? throw new Exception(msg.Message);
            ResponseUpdateAccAdmin obj = new()
            {
                FullName = user.FullName,
                UrlPicture = user.UrlPicture,
                Roles = user.Roles,
                UserId = userId,
                DepartmentName = null,
                Email = user.Email,
                CreatedDate = user.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss"),
                UserName = user.UserName
            };
            return Ok(obj);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Tạo account admin để đăng nhập tài khoản admin
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(RequestCreateUser obj)
    {
        msg.Message = "Tạo tài khoản thất bại :)";
        try
        {
            obj.UserName = Utilities.RemoveHTMLTag(obj.UserName!);
            // check username
            bool checkusername = await _userRepositories.CheckUserName(obj.UserName);
            if (checkusername)
            {
                msg.Message = "Tài khoản này đã tồn tại :)";
                throw new Exception(msg.Message);
            }
            obj.Password = Utilities.RemoveHTMLTag(obj.Password!);
            obj.FullName = Utilities.RemoveHTMLTag(obj.FullName!);
            obj.Email = Utilities.RemoveHTMLTag(obj.Email!);
            obj.UrlPicture = Utilities.RemoveHTMLTag(obj.UrlPicture!);
            string passwordSalt = Utilities.GeneratePasswordSalt();
            string password = Utilities.GeneratePasswordHash(obj.Password, passwordSalt);
            Guid guid = new();
            guid = Guid.NewGuid();
            UserAdmins user = new()
            {
                UserId = guid,
                UserName = obj.UserName,
                FullName = obj.FullName,
                Email = obj.Email,
                Roles = obj.Roles,
                UrlPicture = obj.UrlPicture,
                DepartmentId = obj.DepartmentId,
                IsDeleted = false,
                IsShow = true,
                IsActive = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                LockDate = DateTime.Now,
                CountPassFail = 0,
                IsLock = false,
                PasswordSalt = passwordSalt,
                Password = password
            };
            int rs = await _userRepositories.Insert(user);
            if (rs == 0) throw new Exception(msg.Message);
            msg.Message = "Tạo tài khoản thành công :3";
            AddLogAdmin("/api/admin/User", "Tạo tài khoản " + user.UserName, "Create-User");
            return Ok(msg);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Sửa account admin để đăng nhập tài khoản admin
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update(RequestUpdateUser obj)
    {
        msg.Message = "Cập nhật tài khoản thất bại :)";
        try
        {
            obj.FullName = Utilities.RemoveHTMLTag(obj.FullName!);
            obj.Email = Utilities.RemoveHTMLTag(obj.Email!);
            obj.UrlPicture = Utilities.RemoveHTMLTag(obj.UrlPicture!);
            UserAdmins? user = await _userRepositories.GetByUserId(obj.UserId!) ?? throw new Exception(msg.Message);
            user.FullName = obj.FullName;
            user.Email = obj.Email;
            user.UrlPicture = obj.UrlPicture;
            user.DepartmentId = obj.DepartmentId;
            user.Roles = obj.Roles;
            user.ModifiedDate = DateTime.Now;
            int rs = await _userRepositories.Update(user);
            if (rs == 0) throw new Exception(msg.Message);
            msg.Message = "Cập nhật tài khoản thành công :3";
            AddLogAdmin("/api/admin/User", "Cập nhật tài khoản " + user.UserName, "Update-User");
            return Ok(msg);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Cập nhật tài khoản đăng nhập
    /// </summary>
    [HttpPatch("UpdateAccountLogin")]
    public async Task<IActionResult> UpdateAccountLogin([FromForm] RequestUpdateAccountLogin obj)
    {
        msg.Message = "Cập nhật tài khoản thất bại :)";
        try
        {
            if (string.IsNullOrEmpty(obj.FullName) || string.IsNullOrEmpty(obj.Email))
            {
                msg.Message = "Vui lòng nhập các trường bắt buộc :)";
                throw new Exception(msg.Message);
            }
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            string? userId = GetUserAdminByToken(token);
            if (string.IsNullOrEmpty(userId)) throw new Exception(msg.Message);

            UserAdmins? user = await _userRepositories.GetByUserId(userId) ?? throw new Exception(msg.Message);
            obj.FullName = Utilities.RemoveHTMLTag(obj.FullName!);
            obj.Email = Utilities.RemoveHTMLTag(obj.Email!);
            string? url = string.Empty;
            if (obj.File != null)
            {
                UploadFileModel file = HandleFiles.UploadFile(obj.File!, "image");
                if (file != null)
                {
                    url = file.UrlPicture;
                }
            }
            else
            {
                url = user.UrlPicture;
            }
            ResponseAccountUpdateLogin userRes = new()
            {
                FullName = obj.FullName,
                Email = obj.Email,
                UrlPicture = url,
                UserId = userId
            };
            int rs = await _userRepositories.UpdateForValue(userRes, "UserAdmins");
            if (rs == 0) throw new Exception(msg.Message);
            msg.Message = "Cập nhật tài khoản thành công :3";
            msg.Obj = new
            {
                userId,
                obj.FullName,
                obj.Email,
                UrlPicture = url
            };
            AddLogAdmin("/account", "Cập nhật tài khoản " + userId, "Update-User");
            return Ok(msg);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }

    /// <summary>
    /// Xoá account admin để đăng nhập tài khoản admin
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> Deleted(string UserId)
    {
        msg.Message = "Xoá tài khoản thất bại :)";
        try
        {
            UserAdmins? user = await _userRepositories.GetByUserId(UserId) ?? throw new Exception(msg.Message);
            user.IsDeleted = true;
            user.ModifiedDate = DateTime.Now;

            int rs = await _userRepositories.Update(user);
            if (rs == 0) throw new Exception(msg.Message);
            msg.Message = "Xoá tài khoản thành công :3";
            return Ok(msg);
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return NotFound(msg);
        }
    }
}