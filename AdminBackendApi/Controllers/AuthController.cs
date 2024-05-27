using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminBackendApi
{
    public class AuthController : BaseController
    {
        private readonly UserRepositories _userRepositories = new(WebConfig.ConnectionString!);

        /// <summary>
        /// Đăng nhập theo tài khoản và mật khẩu trả về mess và token, refreshtoken lưu trong cookie httponly
        /// </summary>
        [HttpPost("Login")]
        public async Task<ActionResult> Login(RequestLogin req)
        {
            ResponseLogin res = new()
            {
                Message = "Đăng nhập thất bại :)"
            };

            try
            {
                if (string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Password))
                {
                    res.Message = "Vui lòng nhập các thông tin bắt buộc";
                    return Ok(res);
                }

                UserAdmins? user = await _userRepositories.GetLogin(req.Username);
                if (user == null)
                {
                    res.Message = "Tài khoản mật khẩu không chính xác:)";
                    return Ok(res);
                }
                if (!user.IsActive)
                {
                    res.Message = "Tài khoản chưa được kích hoạt :)";
                    return Ok(res);
                }
                if (user.IsLock)
                {
                    res.Message = "Tài khoản của bạn đã bị khóa :)";
                    return Ok(res);
                }
                string passwordSha256 = Utilities.GeneratePasswordHash(req.Password, user.PasswordSalt!);
                if (passwordSha256 != user.Password)
                {
                    res.Message = "Tài khoản mật khẩu không chính xác:)";
                    if (user.CountPassFail <= 6)
                    {
                        int count = user.CountPassFail + 1;
                        int rs = await _userRepositories.UpdateCountFailLogin(count, user.UserName!);
                        if (rs == 0) return Ok(res);
                        if (count == 6)
                        {
                            int rs2 = await _userRepositories.UpdateIsLockUser(user.UserName!);
                            if (rs2 == 0) return Ok(res);
                        }
                        return Ok(res);
                    }
                    return Ok(res);
                }

                int rs3 = await _userRepositories.UpdateCountFailLogin(0, user.UserName!);
                if (rs3 == 0) return Ok(res);
                string token = CreateToken(user);
                CreateToken(user, "RefreshToken");
                res.Token = token;
                res.Error = false;
                res.Message = "Đăng nhập thành công :3";
                return Ok(res);
            }
            catch (Exception e)
            {
                Utilities.AddLogError(e);
                return NotFound(res);
            }
        }

        /// <summary>
        /// Đăng xuất tài khoản admin -> Xoá cookies
        /// </summary>
        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("RefreshToken");
            MessagesModel msg = new()
            {
                Error = false,
                Message = "Đăng xuất thành công :3"
            };
            return Ok(msg);
        }

        /// <summary>
        /// RefreshToken Tạo mã token mới khi token cũ hết hạn
        /// </summary>
        [HttpPost("RefreshToken")]
        public ActionResult RefreshToken()
        {
            MessagesModel msg = new()
            {
                Message = "Invalid refresh token :)"
            };
            try
            {
                string refreshToken = Request.Cookies["RefreshToken"]!;
                if (string.IsNullOrEmpty(refreshToken)) return Unauthorized(msg);
                SecurityKey securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebConfig.KeyRefresh!));
                DateTime timeout = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfig.TimeoutRefresh!));
                TokenValidationParameters validationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = [securitykey],
                    ValidateIssuer = true,
                    ValidIssuer = WebConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = WebConfig.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(WebConfig.TimeoutRefresh))
                };
                CheckTokenModel checkToken = CheckToken(refreshToken, validationParameters);
                string tokenNew = CreateToken(checkToken.User!);
                CreateToken(checkToken.User!, "RefreshToken");
                ResponseRefreshToken rs = new()
                {
                    Token = tokenNew
                };

                return Ok(rs);
            }
            catch (Exception e)
            {
                Utilities.AddLogError(e);
                return NotFound(msg);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword(RequestChangePassword req)
        {
            MessagesModel msg = new()
            {
                Message = "Đổi mật khẩu thất bại :)"
            };
            try
            {
                if (string.IsNullOrEmpty(req.PasswordOld) || string.IsNullOrEmpty(req.PasswordNew) || string.IsNullOrEmpty(req.PasswordConfirm))
                {
                    msg.Message = "Vui lòng nhập các trường bắt buộc :)";
                    return Ok(msg);
                }
                if (req.PasswordNew != req.PasswordConfirm)
                {
                    msg.Message = "Mật khẩu xác nhận không chính xác :)";
                    return Ok(msg);
                }
                req.PasswordOld = Utilities.RemoveHTMLTag(req.PasswordOld);
                req.PasswordNew = Utilities.RemoveHTMLTag(req.PasswordNew);
                req.PasswordConfirm = Utilities.RemoveHTMLTag(req.PasswordConfirm);

                string token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                string? userId = GetUserAdminByToken(token);
                if (string.IsNullOrEmpty(userId)) return Ok(msg);
                UserAdmins? user = await _userRepositories.GetByUserId(userId);
                if (user == null) return Ok(msg);
                string passwordSha256 = Utilities.GeneratePasswordHash(req.PasswordOld!, user.PasswordSalt!);
                if (passwordSha256 != user.Password)
                {
                    msg.Message = "Mật khẩu cũ không chính xác :)";
                    return Ok(msg);
                }
                string passwordSalt = Utilities.GeneratePasswordSalt();
                string password = Utilities.GeneratePasswordHash(req.PasswordNew, passwordSalt);
                object passwordChange = new
                {
                    Password = password,
                    PasswordSalt = passwordSalt,
                    UserId = userId,
                    ModifiedDate = DateTime.Now
                };
                int rs = await _userRepositories.UpdateForValue(passwordChange, "UserAdmins");
                if (rs == 0) return Ok(msg);
                msg.Error = false;
                msg.Message = "Đổi mật khẩu thành công <3";
                return Ok(msg);
            }
            catch (Exception e)
            {
                Utilities.AddLogError(e);
                return NotFound(msg);
            }
        }

        /// <summary>
        /// Xoá tài khoản
        ///  </summary>   
        [HttpDelete("DeleteAccount")]
        public async Task<ActionResult> DeleteAccount()
        {
            MessagesModel msg = new()
            {
                Message = "Xoá tài khoản thất bại :)"
            };
            try
            {
                string token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
                string? userId = GetUserAdminByToken(token);
                if (string.IsNullOrEmpty(userId)) return Ok(msg);
                UserAdmins? user = await _userRepositories.GetByUserId(userId);
                if (user == null) return Ok(msg);
                object obj = new{
                    UserId = userId
                };
                int rs = await _userRepositories.Delete(obj, "UserAdmins");
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

        /// <summary>
        /// Tạo mã Token và RefreshToken
        /// </summary>
        private string CreateToken(UserAdmins user, string name = "Token")
        {
            JwtSecurityToken token = new();
            List<Claim> claims =
            [
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Roles!),
                new Claim(JwtRegisteredClaimNames.Aud, WebConfig.Issuer!),
                new Claim(JwtRegisteredClaimNames.Iss, WebConfig.Audience!)
            ];

            if (name == "Token")
            {
                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebConfig.KeyToken!));
                SigningCredentials? creds = new(securityKey, SecurityAlgorithms.HmacSha256);
                DateTime timeout = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfig.TimeoutToken!));
                token = new JwtSecurityToken(
                    WebConfig.Issuer,
                    WebConfig.Audience,
                    claims,
                    expires: timeout,
                    signingCredentials: creds
                );
            }
            else
            {
                SecurityKey securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebConfig.KeyRefresh!));
                SigningCredentials? creds = new(securitykey, SecurityAlgorithms.HmacSha256);
                DateTime timeout = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfig.TimeoutRefresh!));
                token = new JwtSecurityToken(
                    WebConfig.Issuer,
                    WebConfig.Audience,
                    claims,
                    expires: timeout,
                    signingCredentials: creds
                );
            }

            string? jwt = new JwtSecurityTokenHandler().WriteToken(token);

            if (name == "RefreshToken")
            {
                DateTime timeout = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfig.TimeoutRefresh!));
                Response.Cookies.Append("RefreshToken", jwt, new CookieOptions
                {
                    HttpOnly = true,
                    Path = "/",
                    Expires = timeout,
                    Secure = true // nếu là http thì đặt thành false
                });
            }
            return jwt;
        }

        /// <summary>
        /// Kiểm tra mã Token và RefreshToken
        /// </summary>
        private CheckTokenModel CheckToken(string token, TokenValidationParameters para)
        {
            CheckTokenModel check = new()
            {
                User = new(),
                IsToken = false
            };
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();
                tokenHandler.ValidateToken(token, para, out var validatedToken);
                var code = tokenHandler.ReadToken(token) as JwtSecurityToken;
                string userId = code!.Claims.FirstOrDefault(x => x.Type == "jti")!.Value;
                string role = code!.Claims.FirstOrDefault(x => x.Type == "jti")!.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    UserAdmins? user = _userRepositories.GetRoleByUserId(userId);
                    if (user != null)
                    {
                        check.User.UserId = Guid.Parse(userId);
                        check.User.Roles = user.Roles;
                        check.IsToken = true;
                    }
                }

                return check;
            }
            catch (Exception e)
            {
                Utilities.AddLogError(e);
                return check;
            }
        }
    }
}