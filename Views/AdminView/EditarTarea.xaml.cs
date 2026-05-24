using DocumentFormat.OpenXml.InkML;
using System.Windows;
using System.Windows.Controls;
using TDL.Data;
using TDL.Models;
using TDL.Repositories;
using TDL.Services;
using TDL.ViewModel;

namespace TDL
{
    public partial class EditarTarea : UserControl
    {
        private readonly TareaService _tareaService;

        private TareaVM tareaSeleccionada;

        public EditarTarea()
        {
            InitializeComponent();

            var context = new AppDbContext();

            var tarearepo = new TareaRepository(context);
            var historialRepo = new HistorialAccionRepository(context);

            var historialService = new HistorialAccionService(historialRepo);



            _tareaService = new TareaService(tarearepo, historialService);

            CargarTareas();

            CargarCombos();
        }

       
        private void CargarTareas()
        {
            dgtareas.ItemsSource = _tareaService.ObtenerTareas();
        }

      

        private void CargarCombos()
        {
            var context = new AppDbContext();

            cbestado.ItemsSource = context.Estados
                .Where(x => x.ID < 6)
                .ToList();

            cbprioridad.ItemsSource = context.Prioridades.ToList();

            cbtecnico.ItemsSource = context.Usuarios
                .Where(x => x.ID_rol == 2)
                .ToList();
        }
        private void LimpiarFormulario()
        {
            txttitulo.Clear();

            txtdescripcion.Clear();

            txtjustificacion.Clear();

            dpfecha.SelectedDate = null;

            cbestado.SelectedIndex = -1;

            cbprioridad.SelectedIndex = -1;

            cbtecnico.SelectedIndex = -1;

            tareaSeleccionada = null;
        }

  

        private void dgtareas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tareaSeleccionada = dgtareas.SelectedItem as TareaVM;

            if (tareaSeleccionada == null)
                return;

            txttitulo.Text = tareaSeleccionada.Titulo;

            txtdescripcion.Text = tareaSeleccionada.Descripcion;

            txtjustificacion.Text = tareaSeleccionada.Justificacion;

            dpfecha.SelectedDate = tareaSeleccionada.Fecha_limite;

            cbestado.Text = tareaSeleccionada.Estado;

            cbprioridad.Text = tareaSeleccionada.Prioridad;

            cbtecnico.Text = tareaSeleccionada.Tecnico;
        }

      

        private void btnguardar_Click(object sender, RoutedEventArgs e)
        {
            if (tareaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una tarea");

                return;
            }

            try
            {
                var tarea = _tareaService
                    .ObtenerPorID(tareaSeleccionada.ID_tarea);

                tarea.Titulo = txttitulo.Text;

                tarea.Descripcion = txtdescripcion.Text;

                tarea.Justificacion = txtjustificacion.Text;

                tarea.Fecha_limite = dpfecha.SelectedDate.Value;

                tarea.ID_estado = (int)cbestado.SelectedValue;

                tarea.ID_prioridad = (int)cbprioridad.SelectedValue;

                tarea.ID_tecnico = (int)cbtecnico.SelectedValue;

                _tareaService.EditarTarea(tarea);

                MessageBox.Show("Tarea actualizada");

                CargarTareas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        
        private void btneliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tareaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una tarea");

                return;
            }

            var resultado = MessageBox.Show(
                "¿Seguro que desea eliminar esta tarea?",
                "Confirmar",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultado != MessageBoxResult.Yes)
                return;

            try
            {
                _tareaService.EliminarTarea(tareaSeleccionada.ID_tarea);

                LimpiarFormulario();

                CargarTareas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}