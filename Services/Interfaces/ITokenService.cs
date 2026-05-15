using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDL.Services.Interfaces
{
    public interface ITokenService
    {
        Task<bool> EnviarTokenAsync(
            string username);

        bool ValidarToken(
            string username,
            string token);
    }
}
