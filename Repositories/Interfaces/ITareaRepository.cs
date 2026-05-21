using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDL.Models;
using TDL.ViewModel;

namespace TDL.Repositories.Interfaces
{
    public interface ITareaRepository
    {
        // =========================================
        // OBTENER TODAS LAS TAREAS
        // =========================================
        List<TareaVM> ObtenerTodass();

        // =========================================
        // OBTENER TAREAS POR TECNICO
        // =========================================
        List<Tarea> ObtenerPorTecnico(int idTecnico);

        // =========================================
        // BUSCAR TAREAS DE TECNICO
        // =========================================
        List<TareaVM> ObtenerPorTecnicoBuscar(
            int idTecnico,
            string texto);

        // =========================================
        // BUSCAR GENERAL
        // =========================================
        List<TareaVM> Buscar(string texto);

        // =========================================
        // OBTENER POR ID
        // =========================================
        Tarea? ObtenerPorID(int id);

        // =========================================
        // ACTUALIZAR ESTADO
        // =========================================
        void Actualizar(Tarea tarea);

        // =========================================
        // REGISTRAR HISTORIAL
        // =========================================
        void RegistrarHistorial(
            int idTarea,
            int idEstado,
            string justificacion);

        // =========================================
        // OBTENER TECNICOS
        // =========================================
        List<Usuarios> ObtenerTecnico();

        // =========================================
        // AGREGAR TAREA
        // =========================================
        void Agregar(Tarea tarea);

        // =========================================
        // EDITAR TAREA
        // =========================================
        void EditarTarea(Tarea tarea);

        // =========================================
        // ELIMINAR TAREA
        // =========================================
        void EliminarTarea(int id);
    }
}
