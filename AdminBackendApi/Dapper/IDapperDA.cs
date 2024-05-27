namespace AdminBackendApi;

internal interface IDapperDA
{
    Task<int> Delete(dynamic id, string nameTable);
    Task<int> Insert<T>(T obj);
    Task<int> Update<T>(T obj);
    int InsertNoId<T>(T obj);
    int UpdateNoId<T>(T obj);
    int DeleteNoId<T>(T obj);

}
