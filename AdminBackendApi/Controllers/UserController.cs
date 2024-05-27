﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminBackendApi;
[Authorize]
public class UserController : BaseController
{
    private readonly UserRepositories _userRepositories = new(WebConfig.ConnectionString!);
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
            return NotFound("Tải thất bại :)");
        }
    }

    /// <summary>
    /// Lấy ra user theo token
    /// </summary>
    [HttpGet("GetUserHeader")]
    public async Task<IActionResult> GetUser()
    {
        MessagesModel msg = new() {
            Message = "Tải thất bại :)"
        };
        try
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            string? userId = GetUserAdminByToken(token);
            if (string.IsNullOrEmpty(userId)) return Ok(msg);
            UserAdmins? user = await _userRepositories.GetUserHeaderByUserId(userId);
            if (user == null) return Ok(msg);
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
         MessagesModel msg = new() {
            Message = "Tải thất bại :)"
        };
        try
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            string? userId = GetUserAdminByToken(token);
            if (string.IsNullOrEmpty(userId)) return Ok(msg);
            UserAdmins? user = await _userRepositories.GetUserUpdateByUserId(userId);
            if (user == null) return Ok(msg);
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
            return Ok(msg);
        }
    }

    /// <summary>
    /// Tạo account admin để đăng nhập tài khoản admin
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(RequestCreateUser obj)
    {
        MessagesModel msg = new()
        {
            Message = "Tạo tài khoản thất bại :)"
        };
        try
        {
            obj.UserName = Utilities.RemoveHTMLTag(obj.UserName!);
            // check username
            bool checkusername = await _userRepositories.CheckUserName(obj.UserName);
            if (checkusername)
            {
                msg.Message = "Tài khoản này đã tồn tại :)";
                return BadRequest(msg);
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
            if (rs == 0) return BadRequest(msg);
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
        MessagesModel msg = new()
        {
            Message = "Cập nhật tài khoản thất bại :)"
        };
        try
        {
            obj.FullName = Utilities.RemoveHTMLTag(obj.FullName!);
            obj.Email = Utilities.RemoveHTMLTag(obj.Email!);
            obj.UrlPicture = Utilities.RemoveHTMLTag(obj.UrlPicture!);
            UserAdmins? user = await _userRepositories.GetByUserId(obj.UserId!);
            if (user == null) return Ok(msg);
            user.FullName = obj.FullName;
            user.Email = obj.Email;
            user.UrlPicture = obj.UrlPicture;
            user.DepartmentId = obj.DepartmentId;
            user.Roles = obj.Roles;
            user.ModifiedDate = DateTime.Now;
            int rs = await _userRepositories.Update(user);
            if (rs == 0) return Ok(msg);
            msg.Error = false;
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
        MessagesModel msg = new()
        {
            Message = "Cập nhật tài khoản thất bại :)"
        };
        try
        {
            if (string.IsNullOrEmpty(obj.FullName) || string.IsNullOrEmpty(obj.Email))
            {
                msg.Message = "Vui lòng nhập các trường bắt buộc :)";
                return Ok(msg);
            }
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            string? userId = GetUserAdminByToken(token);
            if (string.IsNullOrEmpty(userId)) return Ok(msg);

            UserAdmins? user = await _userRepositories.GetByUserId(userId);
            if (user == null) return Ok(msg);
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
            }else {
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
            if (rs == 0) return Ok(msg);
            msg.Error = false;
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
        MessagesModel msg = new()
        {
            Message = "Xoá tài khoản thất bại :)"
        };

        try
        {
            UserAdmins? user = await _userRepositories.GetByUserId(UserId);
            if (user == null) return Ok(msg);
            user.IsDeleted = true;
            user.ModifiedDate = DateTime.Now;

            int rs = await _userRepositories.Update(user);
            if (rs == 0) return Ok(msg);
            msg.Error = false;
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