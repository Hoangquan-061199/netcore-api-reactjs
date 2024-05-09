using System.Data.SqlClient;
using Dapper;

namespace AdminBackendApi;

internal class LogAdminRepositories(string connectionSql)
{
    private readonly DapperDA _dapperDa = new(connectionSql);

    /// <summary>
    /// Insert vào database
    /// </summary>
    internal async Task<int> Insert(LogAdmins obj) => await _dapperDa.Insert(obj);

    /// <summary>
    /// Xoá log theo id
    /// </summary>
    internal async Task<int> Delete(int id)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<int>? rs = await connect.QueryAsync<int>("DELETE FROM LogAdmins Where ID = @id", new { id });
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
    /// Lấy ra tất cả bản ghi Log
    /// </summary>
    internal async Task<List<LogAdmins>?> GetAll()
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<LogAdmins>? rs = await connect.QueryAsync<LogAdmins>("SELECT ID,Action,Url,UserID,DateCreated,Content,UserLogin,ClassControl FROM LogAdmins");
            connect.Close();
            return rs != null && rs.Any() ? rs.ToList() : null;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }
}
