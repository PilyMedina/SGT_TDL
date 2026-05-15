using TDL.Helpers;
using TDL.Models;
using TDL.Repositories;
using TDL.Services.Interfaces;
namespace TDL.Services
{
    public class LoginService : ILoginService
    {
        private readonly UsuarioRepository _repo;
        public LoginService(UsuarioRepository repo)
        {
            _repo = repo;
        }
        public Usuarios Login(string usuario, string password)
        {
            var user = _repo.ObtenerPorUsuario(usuario);

            if (user == null)
            {
                throw new Exception("Usuario no existe");
            }

            string passwordEncriptada = SeguridadHelper.Encriptar(password);

            if (user.Passsword != passwordEncriptada)
            {
                throw new Exception("Contraseña Incorrecta");
            }

            return user;
        }
    }
}
