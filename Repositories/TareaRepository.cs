using System.Data.Entity;
using TDL.Data;
using TDL.Models;
using TDL.ViewModel;
using TDL.Repositories;
using TDL.Repositories.Interfaces;

namespace TDL.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly AppDbContext _context;

        public TareaRepository(AppDbContext context)
        {
            _context = context;
        }

        // =========================================
        // OBTENER TODAS LAS TAREAS
        // =========================================
        public List<TareaVM> ObtenerTodass()
        {
            return (from t in _context.Tareas
                    join e in _context.Estados on t.ID_estado equals e.ID
                    join p in _context.Prioridades on t.ID_prioridad equals p.ID
                    join u in _context.Usuarios on t.ID_tecnico equals u.ID

                    where t.ID_estado != 6
                    orderby t.ID_tarea descending

                    select new TareaVM
                    {
                        ID_tarea = t.ID_tarea,

                        Titulo = t.Titulo,
                        Descripcion = t.Descripcion,
                        Estado = e.Estado,
                        Prioridad = p.Prioridad,
                        Tecnico = u.Nombre,
                        Justificacion = t.Justificacion,
                        Fecha_apertura = t.Fecha_apertura,
                        Fecha_limite = t.Fecha_limite,
                    })

                    .ToList();
        }

        // =========================================
        // OBTENER TAREAS POR TECNICO
        // =========================================
        public List<Tarea> ObtenerPorTecnico(int idTecnico)
        {
            return _context.Tareas

                .Where(t => t.ID_tecnico == idTecnico  && t.ID_estado !=4  && t.ID_estado !=6)
                .Include(t => t.Estado)
                .Include(t => t.Prioridad)
                .OrderByDescending(t => t.ID_tarea)
                .ToList();
        }

        // =========================================
        // BUSCAR TAREAS DE TECNICO
        // =========================================
        public List<TareaVM> ObtenerPorTecnicoBuscar(int idTecnico, string texto)
        {
            DateTime fecha;

            bool esFecha = DateTime.TryParse(texto, out fecha);

            var query = from t in _context.Tareas
                        join e in _context.Estados on t.ID_estado equals e.ID
                        join p in _context.Prioridades on t.ID_prioridad equals p.ID
                        join u in _context.Usuarios on t.ID_tecnico equals u.ID

                        where t.ID_tecnico == idTecnico && t.ID_estado != 6

                        select new { t, e, p, u };

            if (!string.IsNullOrWhiteSpace(texto))
            {
                query = query.Where(x =>
                    x.t.Titulo.Contains(texto) ||
                    x.t.Descripcion.Contains(texto) ||
                    x.e.Estado.Contains(texto) ||
                    x.p.Prioridad.Contains(texto) ||
                    x.u.Nombre.Contains(texto) ||
                    (x.t.Justificacion != null &&
                     x.t.Justificacion.Contains(texto)) ||

                    (esFecha && (
                        x.t.Fecha_apertura.Date == fecha.Date ||
                        x.t.Fecha_limite.Date == fecha.Date
                    ))
                );
            }

            return query
                .OrderByDescending(x => x.t.ID_tarea)
               

                .Select(x => new TareaVM
                {
                    ID_tarea = x.t.ID_tarea,

                    Titulo = x.t.Titulo,
                    Tecnico = x.u.Nombre,
                    Descripcion = x.t.Descripcion,
                    Estado = x.e.Estado,
                    Prioridad = x.p.Prioridad,
                    Justificacion = x.t.Justificacion,
                    Fecha_apertura = x.t.Fecha_apertura,
                    Fecha_limite = x.t.Fecha_limite,
                })

                .ToList();
        }

        // =========================================
        // BUSCAR GENERAL
        // =========================================
        public List<TareaVM> Buscar(string texto)
        {
            DateTime fecha;

            bool esFecha = DateTime.TryParse(texto, out fecha);

            return (from t in _context.Tareas
                    join e in _context.Estados on t.ID_estado equals e.ID
                    join p in _context.Prioridades on t.ID_prioridad equals p.ID
                    join u in _context.Usuarios on t.ID_tecnico equals u.ID

                    where e.ID !=6
                    &&(
                    t.Titulo.Contains(texto)
                       || t.Descripcion.Contains(texto)
                       || e.Estado.Contains(texto)
                       || p.Prioridad.Contains(texto)
                       || u.Nombre.Contains(texto)
                       || t.Justificacion.Contains(texto)

                       || (esFecha && (
                              t.Fecha_apertura.Date == fecha.Date
                           || t.Fecha_limite.Date == fecha.Date
                          )))

                    orderby t.ID_tarea descending

                    select new TareaVM
                    {
                        ID_tarea = t.ID_tarea,

                        Titulo = t.Titulo,
                        Tecnico = u.Nombre,
                        Descripcion = t.Descripcion,
                        Estado = e.Estado,
                        Prioridad = p.Prioridad,
                        Justificacion = t.Justificacion,
                        Fecha_apertura = t.Fecha_apertura,
                        Fecha_limite = t.Fecha_limite,
                    })

                    .ToList();
        }

        // =========================================
        // OBTENER POR ID
        // =========================================
        public Tarea? ObtenerPorID(int id)
        {
            return _context.Tareas
                .FirstOrDefault(t => t.ID_tarea == id);
        }

        // =========================================
        // ACTUALIZAR ESTADO
        // =========================================
        public void Actualizar(Tarea tarea)
        {
            var tareaDb = _context.Tareas
                .FirstOrDefault(t => t.ID_tarea == tarea.ID_tarea);

            if (tareaDb != null)
            {
                tareaDb.ID_estado = tarea.ID_estado;
                tareaDb.Justificacion = tarea.Justificacion;
                tareaDb.JustificacionCierre = tarea.JustificacionCierre;

                _context.SaveChanges();
            }
        }

        // =========================================
        // REGISTRAR HISTORIAL
        // =========================================
        public void RegistrarHistorial(int idTarea,
                                       int idEstado,
                                       string justificacion)
        {
            var historial = new CambioEstadoTarea
            {
                ID_tarea = idTarea,
                ID_estado = idEstado,
                Justificacion = justificacion,
                FechaCambio = DateTime.Now
            };

            _context.CambioEstadoTareas.Add(historial);

            _context.SaveChanges();
        }

        // =========================================
        // OBTENER TECNICOS
        // =========================================
        public List<Usuarios> ObtenerTecnico()
        {
            return _context.Usuarios
                .Where(u => u.ID_rol == 2)
                .ToList();
        }

        // =========================================
        // AGREGAR TAREA
        // =========================================
        public void Agregar(Tarea tarea)
        {
            _context.Tareas.Add(tarea);

            _context.SaveChanges();
        }

        // =========================================
        // EDITAR TAREA
        // =========================================
        public void EditarTarea(Tarea tarea)
        {
            var tareaDb = _context.Tareas
                .FirstOrDefault(t => t.ID_tarea == tarea.ID_tarea);

            if (tareaDb == null)
                throw new Exception("La tarea no existe");

            tareaDb.Titulo = tarea.Titulo;
            tareaDb.Descripcion = tarea.Descripcion;
            tareaDb.ID_estado = tarea.ID_estado;
            tareaDb.ID_prioridad = tarea.ID_prioridad;
            tareaDb.ID_tecnico = tarea.ID_tecnico;
            tareaDb.Fecha_limite = tarea.Fecha_limite;
            tareaDb.Justificacion = tarea.Justificacion;
            tareaDb.JustificacionCierre = tarea.JustificacionCierre;

            _context.SaveChanges();
        }
        public void EliminarTarea(int id)
        {
            var tarea = _context.Tareas
                .FirstOrDefault(t => t.ID_tarea == id);

            if (tarea == null)
                throw new Exception("La tarea no existe");

            _context.Tareas.Remove(tarea);

            _context.SaveChanges();
        }

      
    }
}