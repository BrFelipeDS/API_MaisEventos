using cripto.Models;

namespace cripto.Interfaces
{
    public interface ILoginRepository
    {
        string Logar(string email, string senha);
    }
}
