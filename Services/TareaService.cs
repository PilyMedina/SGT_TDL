using System.Windows;
using TDL.Helpers;
using TDL.Models;
using TDL.Repositories;
using TDL.Services.Interfaces;
using TDL.ViewModel;



namespace TDL.Services
{
    public class TareaService : ITareaService
    {
        private readonly TareaRepository _repo;
        private readonly IHistorialAccionService _historialService;

        public TareaService(TareaRepository repo, IHistorialAccionService historialService)
        {
            _repo = repo;
            _historialService = historialService;
        }
        public void CrearTarea(
            string titulo,
            string descripcion,
            int idPrioridad,
            int idTecnico,
            DateTime fechaLimite)
        {
 
            var tarea = new Tarea
            {
                Titulo = titulo,
                Descripcion = descripcion,
                ID_estado = 1 ,
                ID_prioridad = idPrioridad,
                ID_tecnico = idTecnico,
                Fecha_apertura = DateTime.Now,
                Fecha_limite = fechaLimite,
                Duracion = DateTime.Now
            };
            _repo.Agregar(tarea);

            _historialService.RegistrarMovimiento(
              tarea.ID_tarea,
              "CREAR",
              $"Se creó la tarea {tarea.Titulo}",
              Sesion.UsuarioId);
        }
        //Ver todas las tareas
        public List<TareaVM> ObtenerTareas()
        {
            return _repo.ObtenerTodass();
        }
      

        //Cambiar estado
        public void CambiarEstado(int idTarea, int idEstado, string justificacion)
        {
            var tarea = _repo.ObtenerPorID(idTarea);

            if (tarea == null)
                throw new Exception("Tarea no encontrada");

            tarea.ID_estado = idEstado;

            _repo.Actualizar(tarea);

            _repo.RegistrarHistorial(idTarea, idEstado, justificacion);
        }
        //Es para ver los tecnicos en la view
        public List<Usuarios> ObtenerTecnico()
        {
            return _repo.ObtenerTecnico();
        }
        //Ver todas las tareas de un mismo tecnico
        public List<Tarea> ObtenerTareasPorTecnico(int idTecnico)
        {
            return _repo.ObtenerPorTecnico(idTecnico);

        }
        
        public List<TareaVM> ObtenerTareasPorTecnicoBuscar(int idTecnico, string texto)
        {
            return _repo.ObtenerPorTecnicoBuscar(idTecnico, texto);
        }
        public void ActualizarTarea(Tarea tarea)
        {
            if(tarea == null)
                throw new Exception("La tarea no es válida");

            
            if (string.IsNullOrWhiteSpace(tarea.Justificacion))
                throw new Exception("Debe escribir una justificación");

           
         
            _repo.Actualizar(tarea);
        }
        public List<TareaVM>BuscarTareas(string texto)
        {
            if(string.IsNullOrWhiteSpace(texto))
            {
                return _repo.ObtenerTodass();
            }
            return _repo.Buscar(texto);
        }
 

       
        public List<Tarea> ObtenerPorTecnico(int idTecnico)
        {
            return _repo.ObtenerPorTecnico(idTecnico);
        }

        public void EditarTarea(Tarea tarea)
        {
            _repo.EditarTarea(tarea);
            _historialService.RegistrarMovimiento(
            tarea.ID_tarea,
            "EDITAR",
            $"Se editó la tarea {tarea.Titulo}",
            Sesion.UsuarioId);
        }
        public Tarea ObtenerPorID(int id)
        {
            return _repo.ObtenerPorID(id);
        }
        public void EliminarTarea(int id)
        {
            var tarea = _repo.ObtenerPorID(id);

            if (tarea == null)
            {
                MessageBox.Show("La tarea no existe");
                return;
            }

            tarea.ID_estado = 6;

            _repo.Actualizar(tarea);

            _historialService.RegistrarMovimiento(
                tarea.ID_tarea,
                "ELIMINAR",
                $"Se eliminó la tarea {tarea.Titulo}",
                Sesion.UsuarioId); 
        }

    }
}
