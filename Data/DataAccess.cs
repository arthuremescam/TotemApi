using Oracle.ManagedDataAccess.Client;




public class DataAccess
{
    private string _connectionString;

    public DataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

       public OracleConnection GetOpenConnection()
    {
        OracleConnection connection = new OracleConnection(_connectionString);
        try
        {
            connection.Open();
            Console.WriteLine("Conexão Oracle bem-sucedida!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Falha na conexão Oracle: " + ex.Message);
            // Você pode lidar com a exceção ou lançá-la novamente, se preferir.
            throw;
        }

        return connection;
    }

}