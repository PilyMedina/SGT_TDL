
using TDL.Models;
using TDL.Repositories;
using TDL.Services.Interfaces;


namespace TDL.Services
{
    public  class RegistrarService : IRegistrarService
    {
        private readonly UsuarioRepository _repo;


        public RegistrarService(UsuarioRepository repo)
        {
            _repo = repo;
        }
        public void Registrar( string nombre, string apellido, string password,  string password2, int rol )
        {
            var usuarioExistente = _repo.ObtenerPorNombre(nombre);
            if (password != password2)
            {
                throw new Exception("Las contraseñas no coinciden");
            }
           

            if (usuarioExistente != null)
            
                throw new Exception("El usuario ya existe");

            var nuevoUsuario = new Usuarios
            {
                Usuario = nombre,              
                Nombre = nombre,
                Apellido = apellido,
                Passsword = password,
                ID_rol = rol
            };

            _repo.AgregarUsuario(nuevoUsuario);
        }
        
    }
}
