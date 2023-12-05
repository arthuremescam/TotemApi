using TotemApi.Models;

public class RetornaFilasResult
{
    public IEnumerable<FilaModel> Filas { get; set; }
    public string Valido { get; set; }
    public string TxMensagem { get; set; }
}
