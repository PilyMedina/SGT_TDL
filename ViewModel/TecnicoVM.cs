using System.Collections.ObjectModel;
using TDL.Data;
using TDL.Models;
using TDL.Repositories;
using TDL.Services;
using System.Linq;

namespace TDL.ViewModel
{
    public class TecnicoVM
    {
        public ObservableCollection<TareaVM> Tareas { get; set; }
        public ObservableCollection<Tarea> ListaTareas { get; set; }
        public string NombreTecnico { get; set; }

        private TareaService _service;

        public TecnicoVM(int idTecnico)
        {
            var context = new AppDbContext();

            var repo = new TareaRepository(context);
            _service = new TareaService(repo);

            ListaTareas = new ObservableCollection<Tarea>(
                   _service.ObtenerTareasPorTecnico(idTecnico)
               );

            //  Cargar nombre del técnico
            var tecnico = context.Usuarios.FirstOrDefault(u => u.ID == idTecnico);

            if (tecnico != null)
            {
                NombreTecnico = tecnico.Nombre;
            }

            
        }
    }
}