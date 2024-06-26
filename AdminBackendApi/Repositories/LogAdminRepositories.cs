using System.Data;
using System.Data.SqlClient;
using AdminBackendApi.Models;
using Dapper;

namespace AdminBackendApi.Repositories;

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
            IEnumerable<int>? rs = await connect.QueryAsync<int>("Update LogAdmins SET IsDeleted = 1 Where ID = @id", new { id });
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
    /// Lấy ra các bản ghi 
    /// </summary>
    internal async Task<List<LogAdmins>?> GetListSearch(SearchModel search)
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
                @keyword = search.Keyword,
                search.Sort,
                start,
                @size = search.PageSize
            });
            IEnumerable<LogAdmins> result = await connect.QueryAsync<LogAdmins>("LogAdminListSearch", paras, commandType: CommandType.StoredProcedure);
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
