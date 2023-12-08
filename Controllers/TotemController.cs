using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TotemController : ControllerBase
{
    private readonly ITotemService _totemService;

    public TotemController(ITotemService totemService)
    {
        _totemService = totemService;
    }
   
    [HttpPost("gerar-senha")]
    public IActionResult GerarSenha([FromBody] GerarSenhaRequest request)
    {
        try
        {
            _totemService.GerarSenha(request.codigoMaquina, request.cdFila, request.tpFila, out var valido, out var mensagem, out var cdSenha);

            var response = new GerarSenhaResponse
            {
                valido = valido,
                mensagem = mensagem,
                cdSenha = cdSenha
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPost("retorna-filas")]
    public IActionResult RetornaFilas([FromQuery] string codigoMaquina)
    {
        try
        {
            var result = _totemService.RetornaFilas(codigoMaquina);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPost("retorna-tipos-fila")]
    public IActionResult RetornaTiposFila()
    {
        try
        {
            var tiposFila = _totemService.RetornaTiposFila();
            return Ok(tiposFila);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

}
