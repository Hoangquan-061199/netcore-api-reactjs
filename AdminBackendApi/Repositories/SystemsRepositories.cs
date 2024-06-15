using System.Data;
using System.Data.SqlClient;
using AdminBackendApi.DataMapping;
using AdminBackendApi.Models;
using Dapper;

namespace AdminBackendApi.Repositories;

internal class SystemsRepositories(string connectionSql)
{
    private readonly DapperDA _dapperDa = new(connectionSql);

    /// <summary>
    /// Lấy ra các bản ghi 
    /// </summary>
    internal async Task<List<SystemConfigItem>?> GetListSearch(SearchModel search)
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
            IEnumerable<SystemConfigItem> result = await connect.QueryAsync<SystemConfigItem>("SystemsListSearch", paras, commandType: CommandType.StoredProcedure);
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
    /// Lấy ra bản ghi theo id 
    /// </summary>
    internal async Task<SystemConfigs?> GetById(int id)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<SystemConfigs> result = await connect.QueryAsync<SystemConfigs>("SELECT [ID],[Name],[Address],[Email],[Hotline],[Facebook],[Zalo],[TikTok],[Youtube],[MailSend],[MailTo],[MailPass],[MailServer],[MailPort],[MailSSL],[SEOTitle],[SEOKeyword],[SEODescription],[Favicon],[OgImage],[OgImageAlt],[IndexGoogle],[IsShow],[Lang],[IsDeleted],[CreatedDate] FROM [SystemConfigs] WHERE IsDeleted = 0 and ID = @id", new { id });
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
    /// Kiểm tra ngôn ngữ
    /// </summary>
    internal async Task<int> CheckLang(string lang)
    {
        try
        {
            SqlConnection connect = _dapperDa.GetOpenConnection();
            IEnumerable<int> result = await connect.QueryAsync<int>("SELECT [ID] FROM [SystemConfigs] WHERE IsDeleted = 0 and Lang = @lang", new { lang });
            connect.Close();
            return result != null && result.Any() ? result.ToList()[0] : 0;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return 0;
        }
    }

    /// <summary>
    /// Insert vào database
    /// </summary>
    internal async Task<int> Insert(SystemConfigs obj) => await _dapperDa.Insert(obj);

    /// <summary>
    /// Update vào database
    /// </summary>
    internal async Task<int> Update(dynamic obj, string tableName) => await _dapperDa.UpdateForValue(obj, tableName);
    internal async Task<int> Delete(dynamic obj, string tableName) => await _dapperDa.Delete(obj, tableName);
}
