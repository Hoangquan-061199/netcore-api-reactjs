﻿using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AdminBackendApi;

internal class DepartmentRepositories(string connectionSql)
{
    private readonly DapperDA _dapperDa = new(connectionSql);

    /// <summary>
    /// Lấy ra các bản ghi 
    /// </summary>
    internal async Task<List<DepartmentItems>?> GetListSearch(SearchModel search)
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
            IEnumerable<DepartmentItems> result = await connect.QueryAsync<DepartmentItems>("DepartmentListSearch", paras, commandType: CommandType.StoredProcedure);
            connect.Close();
            return result?.ToList();
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }

    /// <summary>
    /// Lấy ra các bản ghi 
    /// </summary>
    internal async Task<DepartmentItems?> GetById(int id)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<DepartmentItems> result = await connect.QueryAsync<DepartmentItems>("DepartmentListSearch", new { id });
            connect.Close();
            return result?.FirstOrDefault();
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return null;
        }
    }

    /// <summary>
    /// Insert vào database
    /// </summary>
    internal async Task<int> Insert(Departments obj) => await _dapperDa.Update(obj);

    /// <summary>
    /// Update vào database
    /// </summary>
    internal async Task<int> Update(Departments obj) => await _dapperDa.Update(obj);

}
