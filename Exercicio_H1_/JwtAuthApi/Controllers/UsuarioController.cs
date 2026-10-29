using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers;

[ApiController]
[Route("api/usuario")]
[Authorize]
public class UsuarioController : ControllerBase
{
    
    [HttpGet("perfil")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Perfil()
    {
        var nomeUsuario = User.Identity?.Name;

        return Ok(new
        {
            mensagem = $"Olá, {nomeUsuario}! Você acessou uma rota protegida com sucesso.",
            usuario = nomeUsuario
        });
    }
}
