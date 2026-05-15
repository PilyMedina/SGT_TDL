
using TDL.Models;
using TDL.ViewModel;

namespace TDL.Services.Interfaces
{
    public interface ITareaService 
    {
        void CrearTarea(string titulo,
            string descripcion,
            int idPrioridad,
            int idTecnico,
            DateTime fechaLimite);
       
        public List<Usuarios> ObtenerTecnico();
  
        public List<Tarea> ObtenerTareasPorTecnico(int idTecnico);

        public void ActualizarTarea(Tarea tarea);

        public List<Tarea> ObtenerPorTecnico(int idTecnico);
        void EditarTarea(Tarea tarea);
        void EliminarTarea(int id);










}
}
