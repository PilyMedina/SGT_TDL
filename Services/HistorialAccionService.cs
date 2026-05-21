
using TDL.Models;
using TDL.Repositories.Interfaces;
using TDL.Services.Interfaces;

namespace TDL.Services
{
    public class HistorialAccionService : IHistorialAccionService
    {
        private readonly IHistorialAccionRepository _repository;
        public HistorialAccionService (IHistorialAccionRepository repo)
        {
            _repository = repo;
        }
        public void RegistrarMovimiento(
            int idTarea,
            string accion,
            string descripcion,
            int idUsuario)
        {
            var historial = new HistorialAccion
            {
                ID_tarea = idTarea,
                Accion = accion,
                Descripcion = descripcion,
                Fecha = DateTime.Now,
                ID_usuario = idUsuario
            };

            _repository.GuardarMovimiento(historial);
        }

        public List<HistorialAccion> ObtenerHistorial()
        {
            return _repository.ListarHistorial();
        }

    }
}
