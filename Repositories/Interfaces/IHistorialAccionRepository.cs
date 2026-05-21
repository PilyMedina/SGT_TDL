using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDL.Models;

namespace TDL.Repositories.Interfaces
{
    public interface IHistorialAccionRepository
    {
        void GuardarMovimiento(HistorialAccion historial);

        List<HistorialAccion> ListarHistorial();
    }
}
