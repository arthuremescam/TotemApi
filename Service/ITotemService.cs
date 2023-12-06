

public interface ITotemService
{
    RetornaFilasResult RetornaFilas(string codigoMaquina);
    List<TipoFilaModel> RetornaTiposFila();
    void GerarSenha(string codigoMaquina, int cdFila, string tpFila, out string valido, out string mensagem, out string cdSenha);


}