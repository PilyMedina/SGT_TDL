using TDL.Data;
using TDL.Helpers;
using TDL.Models;
using TDL.Repositories;

namespace TDL.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repo;


        public UsuarioService(UsuarioRepository repo)
        {
            _repo = repo;
        }

        public List<Usuarios> ObtenerTecnico()
        {
            return _repo.ObtenerTecnico();
        }
        public bool CambiarContrasena(int usuarioid, string actual, string nueva)
        {
            var usuario = _repo.ObtenerPorId(usuarioid);
            if (usuario == null)
                return false;

            if (usuario.Passsword != SeguridadHelper.Encriptar(actual))
                return false;

            if (string.IsNullOrWhiteSpace(nueva) || nueva.Length < 6)
                return false;

            usuario.Passsword = SeguridadHelper.Encriptar(nueva);


            _repo.Actualizar(usuario);

            return true;

        }
        public void AgregarUsuario(
        string nombre,
        string apellido,
        string password,
        int idRol)
        {
           

            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("Ingrese el nombre");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new Exception("Ingrese el apellido");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Ingrese la contraseña");

            if (idRol <= 0)
                throw new Exception("Seleccione un rol");

         

            var usuario = new Usuarios
            {
                Nombre = nombre.Trim(),

                Apellido = apellido.Trim(),

                Passsword = SeguridadHelper.Encriptar(password.Trim()),

                ID_rol = idRol
            };

           

            _repo.AgregarUsuario(usuario);
        }

        public void EliminarUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Ingrese un usuario");

            var usuarioEncontrado = _repo.ObtenerPorUsuario(usuario.Trim());

            if (usuarioEncontrado == null)
                throw new Exception("El usuario no existe");

            _repo.EliminarUsuario(usuarioEncontrado);
        }




    }
}
