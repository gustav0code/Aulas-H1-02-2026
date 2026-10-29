using System.ComponentModel.DataAnnotations;

namespace JwtAuthApi.Models;


public class LoginRequest
{
    [Required(ErrorMessage = "O usuário é obrigatório.")]
    public string Usuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Senha { get; set; } = string.Empty;
}
