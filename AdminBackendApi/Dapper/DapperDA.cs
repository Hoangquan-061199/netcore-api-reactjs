using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AdminBackendApi;

internal class DapperDA(string conection) : IDapperDA
{
    private readonly string _connectionString = conection;

    public SqlConnection GetOpenConnection()
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();
        return connection;
    }

    public async Task<SqlConnection> GetOpenConnectionAsync()
    {
        SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return connection;
    }

    public async Task<int> UpdateForValue(dynamic obj, string tableName)
    {
        try
        {
            var propertyContainer = ParsePropertiesDynamic(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE [{0}] SET {1} WHERE {2}", tableName, sqlValuePairs, sqlIdPairs);
            await ExecuteAsync(CommandType.Text, sql, propertyContainer.AllPairs);
            return 1;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return 0;
        }
    }


    public async Task<int> Delete(dynamic obj, string tableName)
    {
        try
        {
            var propertyContainer = ParsePropertiesDynamic(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("Delete from [{0}] WHERE {1}", tableName,  sqlIdPairs);
            await ExecuteAsync(CommandType.Text, sql, propertyContainer.AllPairs);
            return 1;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return 0;
        }
    }

    public int DeleteNoId<T>(T obj)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Insert<T>(T obj)
    {
        try
        {
            PropertyContainer propertyContainer = ParseProperties(obj);
            string sql = string.Format("INSERT INTO [{0}] ({1}) VALUES (@{2}) SELECT CAST(scope_identity() AS int)",
                typeof(T).Name,
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(", @", propertyContainer.ValueNames));

            SqlConnection connection = GetOpenConnection();
            var id = await connection.QueryAsync<int>(sql, propertyContainer.ValuePairs, commandType: CommandType.Text);
            SetId(obj, id.First(), propertyContainer.IdPairs);
            return id.First();
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return 0;
        }
    }

    public async Task<int> InsertUserNoId<T>(T obj)
    {
        try
        {
            var propertyContainer = ParsePropertiesAdmin(obj);
            var sql = string.Format("INSERT INTO [{0}] ({1}) VALUES (@{2})",
                typeof(T).Name,
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(", @", propertyContainer.ValueNames));

            var connection = GetOpenConnection();
            var id = await connection.QueryAsync<int>
                (sql, propertyContainer.ValuePairs, commandType: CommandType.Text);
            SetId(obj, id.FirstOrDefault(), propertyContainer.IdPairs);
            return 1;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return 0;
        }
    }

    public async Task<int> Update<T>(T obj)
    {
        try
        {
            var propertyContainer = ParseProperties(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE [{0}] SET {1} WHERE {2}", typeof(T).Name, sqlValuePairs, sqlIdPairs);
            await ExecuteAsync(CommandType.Text, sql, propertyContainer.AllPairs);
            return 1;
        }
        catch (Exception e)
        {
            Utilities.AddLogError(e);
            return 0;
        }
    }

    public int UpdateNoId<T>(T obj)
    {
        throw new NotImplementedException();
    }

    private static PropertyContainer ParseProperties<T>(T obj)
    {
        PropertyContainer propertyContainer = new();

        string? typeName = typeof(T).Name.ToLower();

        string[]? validKeyNames =
            [
                    "id",
                "userid",
                string.Format("{0}id", typeName),
                string.Format("{0}_id", typeName)
                ];

        System.Reflection.PropertyInfo[]? properties = obj!.GetType().GetProperties();
        foreach (System.Reflection.PropertyInfo? property in properties)
        {
            // Skip reference types (but still include string!)
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                continue;

            // Skip methods without a public setter
            if (property.GetSetMethod() == null)
                continue;

            Type? type = property.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type != typeof(string) && type != typeof(int) && type != typeof(bool) && type != typeof(DateTime)
                && type != typeof(double) && type != typeof(decimal) && type != typeof(Nullable) && type != typeof(Guid))
                continue;

            string name = property.Name;
            object? value = property.GetValue(obj, null);

            if (validKeyNames.Contains(name.ToLower()))
            {
                propertyContainer.AddId(name, value!);
            }
            else if (type == typeof(DateTime))
            {
                if (Convert.ToDateTime(value) == DateTime.MinValue)
                {
                    propertyContainer.AddValue(name, null!);
                }
                else
                {
                    propertyContainer.AddValue(name, value!);
                }
            }
            else
            {
                propertyContainer.AddValue(name, value!);
            }
        }
        return propertyContainer;
    }

    private static PropertyContainer ParsePropertiesDynamic(dynamic obj)
    {
        PropertyContainer propertyContainer = new();

        string[]? validKeyNames = ["id", "userid"];

        System.Reflection.PropertyInfo[]? properties = obj!.GetType().GetProperties();
        foreach (System.Reflection.PropertyInfo? property in properties)
        {
            // Skip reference types (but still include string!)
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                continue;

            Type? type = property.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type != typeof(string) && type != typeof(int) && type != typeof(bool) && type != typeof(DateTime)
                && type != typeof(double) && type != typeof(decimal) && type != typeof(Nullable) && type != typeof(Guid))
                continue;

            string name = property.Name;
            object? value = property.GetValue(obj, null);

            if (validKeyNames.Contains(name.ToLower()))
            {
                propertyContainer.AddId(name, value!);
            }
            else if (type == typeof(DateTime))
            {
                if (Convert.ToDateTime(value) == DateTime.MinValue)
                {
                    propertyContainer.AddValue(name, null!);
                }
                else
                {
                    propertyContainer.AddValue(name, value!);
                }
            }
            else
            {
                propertyContainer.AddValue(name, value!);
            }
        }
        return propertyContainer;
    }

    private class PropertyContainer
    {
        private readonly Dictionary<string, object> _ids;
        private readonly Dictionary<string, object> _values;

        #region Properties

        internal IEnumerable<string> IdNames
        {
            get { return _ids.Keys; }
        }

        internal IEnumerable<string> ValueNames
        {
            get { return _values.Keys; }
        }

        internal IEnumerable<string> AllNames
        {
            get { return _ids.Keys.Union(_values.Keys); }
        }

        internal IDictionary<string, object> IdPairs
        {
            get { return _ids; }
        }

        internal IDictionary<string, object> ValuePairs
        {
            get { return _values; }
        }

        internal IEnumerable<KeyValuePair<string, object>> AllPairs
        {
            get { return _ids.Concat(_values); }
        }

        #endregion Properties

        #region Constructor

        internal PropertyContainer()
        {
            _ids = [];
            _values = [];
        }

        #endregion Constructor

        #region Methods

        internal void AddId(string name, object value)
        {
            _ids.Add(name, value);
        }

        internal void AddValue(string name, object value)
        {
            _values.Add(name, value);
        }

        #endregion Methods
    }

    private static void SetId<T>(T obj, int id, IDictionary<string, object> propertyPairs)
    {
        if (propertyPairs.Count == 1)
        {
            string? propertyName = propertyPairs.Keys.First();
            System.Reflection.PropertyInfo? propertyInfo = obj!.GetType().GetProperty(propertyName);
            if (propertyInfo!.PropertyType == typeof(int))
            {
                propertyInfo.SetValue(obj, id, null);
            }
        }
    }

    private static PropertyContainer ParsePropertiesAdmin<T>(T obj)
    {
        var propertyContainer = new PropertyContainer();

        var typeName = typeof(T).Name.ToLower();

        var validKeyNames = new[]
            {
                    "Userid",
                    string.Format("{0}id", typeName), string.Format("{0}_id", typeName)
                };

        var properties = obj!.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Skip reference types (but still include string!)
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                continue;

            // Skip methods without a public setter
            if (property.GetSetMethod() == null)
                continue;

            var type = property.PropertyType;
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type != typeof(string) && type != typeof(int) && type != typeof(bool) && type != typeof(DateTime)
                && type != typeof(double) && type != typeof(decimal) && type != typeof(Nullable) && type != typeof(Guid))
                continue;

            var name = property.Name;
            var value = property.GetValue(obj, null);

            if (validKeyNames.Contains(name.ToLower()))
            {
                propertyContainer.AddId(name, value!);
            }
            else if (type == typeof(DateTime))
            {
                if (Convert.ToDateTime(value) == DateTime.MinValue)
                {
                    propertyContainer.AddValue(name, "");
                }
                else
                {
                    propertyContainer.AddValue(name, value!);
                }
            }
            else
            {
                propertyContainer.AddValue(name, value!);
            }
        }
        return propertyContainer;
    }

    public int InsertNoId<T>(T obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Create a commaseparated list of value pairs on
    /// the form: "key1=@value1, key2=@value2, ..."
    /// </summary>
    private static string GetSqlPairs(IEnumerable<string> keys, string separator = ", ")
    {
        var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
        return string.Join(separator, pairs);
    }
    private static string GetNameSelect(IEnumerable<string> keys, string separator = ", ")
    {
        var pairs = keys.Select(key => string.Format("{0}", key)).ToList();
        return string.Join(separator, pairs);
    }


    private async Task<int> ExecuteAsync(CommandType commandType, string sql, object? parameters = null)
    {
        var connection = await GetOpenConnectionAsync();
        {
            var result = await connection.ExecuteAsync(sql, parameters, commandType: commandType);
            connection.Close();
            return result;
        }
    }
}