using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDL.Models;

namespace TDL.Services.Interfaces
{
    public interface IHistorialAccionService
    {
        void RegistrarMovimiento(
           int idTarea,
           string accion,
           string descripcion,
           int idUsuario);

        List<HistorialAccion> ObtenerHistorial();
    }
}
