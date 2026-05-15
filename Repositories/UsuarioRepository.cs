using TDL.Models;
using TDL.Data;
namespace TDL.Repositories
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;
        //inyectando la DBCONTEXT
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Usuarios> ObtenerTecnico()
        {
            return _context.Usuarios
                .Where(u => u.ID_rol == 2)
                .ToList();
        }
        public Usuarios ObtenerPorNombre(string Nombre)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Nombre == Nombre);
        }
        public void AgregarUsuario (Usuarios usuarios)
        {
            _context.Usuarios.Add(usuarios);
            _context.SaveChanges();
        }
        //NO SE SI DEBO DECLARAR USUARIO CON MAYUS
        public Usuarios ObtenerPorUsuario(string Usuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Usuario == Usuario);
        }
        public Usuarios ObtenerPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public void Actualizar(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void EliminarUsuario(Usuarios usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }



    }
}
