using AdminBackendApi.DataMapping;
using AdminBackendApi.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AdminBackendApi.Repositories;

internal class UserRepositories(string connectionSql)
{
    private readonly DapperDA _dapperDa = new(connectionSql);

    /// <summary>
    /// Insert vào database
    /// </summary>
    internal async Task<int> Insert(UserAdmins obj) => await _dapperDa.InsertUserNoId(obj);

    /// <summary>
    /// Update vào database
    /// </summary>
    internal async Task<int> Update(UserAdmins obj) => await _dapperDa.Update(obj);

    internal async Task<int> Delete(dynamic id, string tableName) => await _dapperDa.Delete(id, tableName);

    /// <summary>
    /// Update vào database
    /// </summary>
    internal async Task<int> UpdateForValue(dynamic obj, string tableName) => await _dapperDa.UpdateForValue(obj, tableName);

    /// <summary>
    /// Kiểm tra username có tồn tại trong db không
    /// </summary>
    internal async Task<bool> CheckUserName(string name)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<string>? rs = await connect.QueryAsync<string>("select UserName from UserAdmins where IsDeleted = 0 and UserName = @name", new { name });
            connect.Close();
            return rs != null && rs.Any();
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return false;
        }
    }

    /// <summary>
    /// Lấy ra user password trong database theo username
    /// </summary>
    internal async Task<UserAdmins?> GetLogin(string name)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<UserAdmins>? rs = await connect.QueryAsync<UserAdmins>("SELECT [UserId],[UserName],[CountPassFail],IsActive,[IsLock],[Password],[PasswordSalt],[Roles] FROM UserAdmins Where UserName = @name", new { name });
            connect.Close();
            return rs != null && rs.Any() ? rs.FirstOrDefault() : null;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }

    /// <summary>
    /// cập nhật số lần đăng nhập thất bại vào database
    /// </summary>
    internal async Task<int> UpdateCountFailLogin(int count, string name)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<int>? rs = await connect.QueryAsync<int>("Update UserAdmins set [CountPassFail] = @count Where UserName = @name", new { name, count });
            connect.Close();
            return 1;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return 0;
        }
    }

    /// <summary>
    /// Cập nhật trạng thái khóa tài khoản vào databse
    /// </summary>
    internal async Task<int> UpdateIsLockUser(string name)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<int>? rs = await connect.QueryAsync<int>("Update UserAdmins set [IsLock] = 1, LockDate = GETDATE() Where UserName = @name", new { name });
            connect.Close();
            return 1;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return 0;
        }
    }

    /// <summary>
    /// Lấy ra Lấy ra Role Theo userid
    /// </summary>
    internal UserAdmins? GetRoleByUserId(string userid)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<UserAdmins>? rs = connect.Query<UserAdmins>("SELECT Roles FROM UserAdmins Where UserId = @userid", new { userid });
            connect.Close();
            return rs != null && rs.Any() ? rs.FirstOrDefault() : null;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }

    /// <summary>
    /// Lấy ra Lấy ra user Theo userid
    /// </summary>
    internal async Task<UserAdmins?> GetByUserId(string userid)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<UserAdmins>? rs = await connect.QueryAsync<UserAdmins>("SELECT [UserId],[UserName],[FullName],[Email],[Roles],[RoleActive],[ModuleAdminIds],[ModuleWebsiteIds],[IsDeleted],[IsShow],[IsActive],[CreatedDate],[ModifiedDate],[LockDate],[CountPassFail],[IsLock],[Password],[PasswordSalt],[DepartmentId],[UrlPicture] FROM [UserAdmins] Where IsDeleted = 0 and UserId = @userid", new { userid });
            connect.Close();
            return rs != null && rs.Any() ? rs.FirstOrDefault() : null;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }

    /// <summary>
    /// Lấy ra Lấy ra user Theo userid
    /// </summary>
    internal async Task<UserAdmins?> GetUserHeaderByUserId(string userid)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<UserAdmins>? rs = await connect.QueryAsync<UserAdmins>("SELECT [FullName],[UrlPicture],[Roles] FROM [UserAdmins] Where IsDeleted = 0 and UserId = @userid", new { userid });
            connect.Close();
            return rs != null && rs.Any() ? rs.FirstOrDefault() : null;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }

    /// <summary>
    /// Lấy ra Lấy ra user Theo userid
    /// </summary>
    internal async Task<UserAdmins?> GetUserUpdateByUserId(string userid)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<UserAdmins>? rs = await connect.QueryAsync<UserAdmins>("SELECT [FullName],[UrlPicture],[Roles],[UserName],[Email],[CreatedDate] FROM [UserAdmins] Where IsDeleted = 0 and UserId = @userid", new { userid });
            connect.Close();
            return rs != null && rs.Any() ? rs.FirstOrDefault() : null;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }


    /// <summary>
    /// Lấy ra Lấy ra username Theo userid
    /// </summary>
    internal string? GetUserNameByIdNoAsync(Guid userid)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<string>? rs = connect.Query<string>("SELECT [UserName] FROM [UserAdmins] Where UserId = @userid", new { userid });
            connect.Close();
            return rs != null && rs.Any() ? rs.FirstOrDefault() : null;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }


    /// <summary>
    /// GetUser by serach parameter
    /// </summary>
    internal async Task<List<UserAdminItems>?> GetListUserBySearch(SearchModel search)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            search.Page = search.Page > 1 ? search.Page : 1;
            int size = search.PageSize > 0 ? search.PageSize : 20;
            int start = (search.Page - 1) * search.PageSize;
            var paras = new DynamicParameters();
            paras.AddDynamicParams(new
            {
                search.Keyword,
                search.Sort,
                start,
                @size = search.PageSize
            });
            var result = await connect.QueryAsync<UserAdminItems>("UserAdminsListSearch", paras, commandType: CommandType.StoredProcedure);
            connect.Close();
            return result?.ToList();
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }
}