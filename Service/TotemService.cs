using Oracle.ManagedDataAccess.Client;
using TotemApi.Models;
using Dapper;
using System.Data;
using Oracle.ManagedDataAccess.Types;

public class TotemService : ITotemService
{
    private readonly string _connectionString;

    public TotemService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public RetornaFilasResult RetornaFilas(string codigoMaquina)
    {
        using (var connection = new OracleConnection(_connectionString))
        {
            connection.Open();

            var command = new OracleCommand("pkg_totem_senha.pr_retorna_filas", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("pCodigoMaquina", OracleDbType.Varchar2, codigoMaquina, ParameterDirection.Input);
            command.Parameters.Add("pFilas", OracleDbType.RefCursor, ParameterDirection.Output);
            command.Parameters.Add("pValido", OracleDbType.Varchar2, ParameterDirection.Output);
            command.Parameters.Add("pTxMensagem", OracleDbType.Varchar2, ParameterDirection.Output);

            command.ExecuteNonQuery();

            // Recuperar os resultados
            var filas = new List<FilaModel>();

            var cursor = (OracleRefCursor)command.Parameters["pFilas"].Value;
            var reader = cursor.GetDataReader();

            while (reader.Read())
            {
                var fila = new FilaModel
                {
                    // Ajuste de acordo com as colunas reais
                    codigoMaquina = reader["cdfila"].ToString(),
                    descricaoFila = reader["dsfila"].ToString()
                };

                filas.Add(fila);
            }

            var valido = command.Parameters["pValido"].Value.ToString();
            var txMensagem = command.Parameters["pTxMensagem"].Value.ToString();

            return new RetornaFilasResult
            {
                Filas = filas,
                Valido = valido,
                TxMensagem = txMensagem
            };
        }
    }




    //     public RetornaFilasResult RetornaFilas(string codigoMaquina)
    // {
    //     using (var connection = new OracleConnection(_connectionString))
    //     {
    //         connection.Open();

    //         var parameters = new DynamicParameters();
    //         parameters.Add("pCodigoMaquina", codigoMaquina, DbType.String, ParameterDirection.Input);
    //         parameters.Add("pFilas", dbType: DbType.Object, direction: ParameterDirection.Output);
    //         parameters.Add("pValido", dbType: DbType.String, direction: ParameterDirection.Output, size: 1);
    //         parameters.Add("pTxMensagem", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

    //         connection.Execute("pkg_totem_senha.pr_retorna_filas", parameters, commandType: CommandType.StoredProcedure);

    //         // Recupere o cursor
    //         var cursor = (IEnumerable<object>)parameters.Get<object>("pFilas");
    //         var filas = cursor.Cast<IDictionary<string, object>>()
    //             .Select(f => new FilaModel
    //             {
    //                 codigoMaquina = f["COLUNA1"] as string,
    //                 descricaoFila = f["COLUNA2"] as string,
    //                 // Adicione mais mapeamentos conforme necessário
    //             });

    //         var valido = parameters.Get<string>("pValido");
    //         var txMensagem = parameters.Get<string>("pTxMensagem");

    //         return new RetornaFilasResult
    //         {
    //             Filas = filas,
    //             Valido = valido,
    //             TxMensagem = txMensagem
    //         };
    //     }
    // }


    // public IEnumerable<FilaModel> RetornaFilas(string codigoMaquina)
    // {
    //     using (var connection = new OracleConnection(_connectionString))
    //     {
    //         connection.Open();

    //         var parameters = new DynamicParameters();
    //         parameters.Add("pCodigoMaquina", codigoMaquina, dbType: (DbType?)OracleDbType.Varchar2, direction: ParameterDirection.Input);

    //         var result = connection.Query<FilaModel>("PKG_TOTEM_SENHA.PR_RETORNA_FILAS", parameters, commandType: CommandType.StoredProcedure);
    //         return result;
    //     }
    // }

    // public SenhaResult GerarSenha(string codigoMaquina, int cdFila, string tpFila)
    // {
    //     using (var connection = new OracleConnection(_connectionString))
    //     {
    //         connection.Open();

    //         var parameters = new DynamicParameters();
    //         parameters.Add("pCodigoMaquina", codigoMaquina, dbType: (DbType?)OracleDbType.Varchar2, direction: ParameterDirection.Input);
    //         parameters.Add("pCdFila", cdFila, dbType: (DbType?)OracleDbType.Int32, direction: ParameterDirection.Input);
    //         parameters.Add("pTpFila", tpFila, dbType: (DbType?)OracleDbType.Varchar2, direction: ParameterDirection.Input);

    //         parameters.Add("pValido", dbType: (DbType?)OracleDbType.Varchar2, direction: ParameterDirection.Output, size: 1);
    //         parameters.Add("pTxMensagem", dbType: (DbType?)OracleDbType.Varchar2, direction: ParameterDirection.Output, size: 255);
    //         parameters.Add("pCdSenha", dbType: (DbType?)OracleDbType.Varchar2, direction: ParameterDirection.Output, size: 50);

    //         connection.Execute("PKG_TOTEM_SENHA.PR_GERAR_SENHA", parameters, commandType: CommandType.StoredProcedure);

    //         var resultado = new SenhaResult
    //         {
    //             valido = parameters.Get<string>("pValido"),
    //             txMensagem = parameters.Get<string>("pTxMensagem"),
    //             cdSenha = parameters.Get<string>("pCdSenha")
    //         };

    //         return resultado;
    //     }
    // }

    // public IEnumerable<TipoFila> RetornaTiposFila()
    // {
    //     using (var connection = new OracleConnection(_connectionString))
    //     {
    //         connection.Open();

    //         var result = connection.Query<TipoFila>("PKG_TOTEM_SENHA.PR_RETORNA_TIPOS_FILA", commandType: CommandType.StoredProcedure);
    //         return result;
    //     }
    // }

    // IEnumerable<TipoFilaModel> ITotemService.RetornaTiposFila()
    // {
    //     throw new NotImplementedException();
    // }
}
