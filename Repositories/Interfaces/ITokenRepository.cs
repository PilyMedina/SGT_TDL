using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDL.Models;

namespace TDL.Repositories.Interfaces
{
    public interface ITokenRepository 
    {
        Usuarios ObtenerUsuarioPorUsername(
        string username);

        void GuardarToken(
            TokenRecuperacion token);

        TokenRecuperacion ObtenerTokenValido(
            int idUsuario,
            string token);

        void GuardarCambios();
    }
}
