using Microsoft.EntityFrameworkCore;
using TDL.Data;
using TDL.Models;
using TDL.Repositories.Interfaces;

namespace TDL.Repositories
{
    public class HistorialAccionRepository : IHistorialAccionRepository
    {
        private readonly AppDbContext _context;
        public HistorialAccionRepository(AppDbContext context)
        {
            _context = context;
        }
        public void GuardarMovimiento(HistorialAccion historial)
        {
            _context.HistorialAcciones.Add(historial);

            _context.SaveChanges();
        }

        public List<HistorialAccion> ListarHistorial()
        {
            return _context.HistorialAcciones
                .Include(h => h.Usuario)
                .Include(h => h.Tarea)
                .OrderByDescending(h => h.Fecha)
                .ToList();
        }
    }
}
