namespace AdminBackendApi;

public class TestRepositories(string connectionSql)
{
        private readonly DapperDA _dapperDa = new(connectionSql);
}
