
using System.Windows;
using TDL.Data;
using TDL.Models;
using TDL.Repositories.Interfaces;

namespace TDL.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext _context;

        public TokenRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public Usuarios ObtenerUsuarioPorUsername(
      string username)
        {
            username = username.Trim();


            var usuarios =
                _context.Usuarios.ToList();

           

            return usuarios.FirstOrDefault(u =>
                u.Usuario != null &&
                u.Usuario.Trim().ToLower() ==
                username.Trim().ToLower());
        }

        public void GuardarToken(
            TokenRecuperacion token)
        {
            _context.TokensRecuperacion
                .Add(token);
        }

        public TokenRecuperacion ObtenerTokenValido(
            int idUsuario,
            string token)
        {
            return _context.TokensRecuperacion
                .FirstOrDefault(t =>
                    t.Id == idUsuario &&
                    t.Token == token &&
                    t.Usado == false &&
                    t.FechaExpiracion > DateTime.Now);
        }

        public void GuardarCambios()
        {
            _context.SaveChanges();
        }




    }
}
