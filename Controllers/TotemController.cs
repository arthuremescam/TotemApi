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

    [HttpPost("retorna-filas/{codigoMaquina}")]
    public IActionResult RetornaFilas(string codigoMaquina)
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
}
