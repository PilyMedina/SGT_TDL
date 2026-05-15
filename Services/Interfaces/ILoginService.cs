using TDL.Models;

namespace TDL.Services.Interfaces
{
    public interface ILoginService
    {
        Usuarios Login(string usuario, string password);
    }
}
