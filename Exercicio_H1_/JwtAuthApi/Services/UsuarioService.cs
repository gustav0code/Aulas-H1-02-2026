namespace JwtAuthApi.Services;

public interface IUsuarioService
{
    bool ValidarCredenciais(string usuario, string senha);
}


public class UsuarioService : IUsuarioService
{
    private const string UsuarioFixo = "aluno";
    private const string SenhaFixa = "senha123";

    public bool ValidarCredenciais(string usuario, string senha)
    {
        return usuario == UsuarioFixo && senha == SenhaFixa;
    }
}
