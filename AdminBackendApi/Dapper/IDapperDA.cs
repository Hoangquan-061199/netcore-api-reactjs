namespace AdminBackendApi;

internal interface IDapperDA
{
    int Delete<T>(T obj);
    Task<int> Insert<T>(T obj);
    Task<int> Update<T>(T obj);
    int InsertNoId<T>(T obj);
    int UpdateNoId<T>(T obj);
    int DeleteNoId<T>(T obj);

}
