using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AdminBackendApi;
[Route($"{WebConfig.UrlStartApiAdmin}[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private readonly UserRepositories _userRepositories = new(WebConfig.ConnectionString!);
    /// <summary>
    /// Ghi log admin khi người dùng quản trị admin
    /// </summary>
    protected void AddLogAdmin(string url, string content, string action)
    {
        try
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            string? userId = GetUserAdminByToken(token);
            if (!string.IsNullOrEmpty(userId))
            {
                Guid id = Guid.Parse(userId);
                string? username = _userRepositories.GetUserNameByIdNoAsync(id);
                if (!string.IsNullOrEmpty(username))
                {
                    LogAdmins logAdmin = new()
                    {
                        Action = action,
                        Link = url,
                        CreatedDate = DateTime.Now,
                        Content = content,
                        UserName = username,
                    };
                    DapperDA dapperDa = new(WebConfig.ConnectionString!);
                    _ = dapperDa.Insert(logAdmin);
                }
            }
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
        }
    }

    /// <summary>
    /// Get UserID and Role by token
    /// </summary>
    protected static string? GetUserAdminByToken(string token)
    {
        try
        {
            SecurityKey securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebConfig.KeyToken!));
            DateTime timeout = DateTime.Now.AddMinutes(Convert.ToInt32(WebConfig.TimeoutToken!));
            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = [securitykey],
                ValidateIssuer = true,
                ValidIssuer = WebConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = WebConfig.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(WebConfig.TimeoutToken))
            };
            JwtSecurityTokenHandler tokenHandler = new();
            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var code = tokenHandler.ReadToken(token) as JwtSecurityToken;
            string userId = code!.Claims.FirstOrDefault(x => x.Type == "jti")!.Value;
            if (string.IsNullOrEmpty(userId)) return null;
            return userId;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }
}