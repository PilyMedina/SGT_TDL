using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TDL.Data;
using TDL.Helpers;
using TDL.Models;
using TDL.Repositories;
using TDL.Services;
using TDL.ViewModel;

namespace TDL
{
    /// <summary>
    /// Lógica de interacción para TecnicoView.xaml
    /// </summary>
    /// 

    public partial class TecnicoView : UserControl
    {
        private TareaService _tareaService;

        public TecnicoView(int idTecnico)
        {
            InitializeComponent();
            var contexto = new AppDbContext();
            var tarearepo = new TareaRepository(contexto);
            var historialRepo = new HistorialAccionRepository(contexto);

            var historialService = new HistorialAccionService(historialRepo);



            _tareaService = new TareaService(tarearepo, historialService);

            DataContext = new TecnicoVM(idTecnico);


            
        }

        private void CerrarSesion_click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new LoginView();
        }
        private void btnguardar(object sender, RoutedEventArgs e)
        {

            var tarea = (sender as Button).DataContext as Tarea;
           
            if (tarea == null) return;
            try
            {
                if (tarea.ID_estado == 1)
                {
                    MessageBox.Show("Debe seleccionar un estado");
                    return;
                }

                if (string.IsNullOrWhiteSpace(tarea.Justificacion))
                {
                    if (tarea.ID_estado == 4)
                        MessageBox.Show("Debe escribir la solución");
                    else
                        MessageBox.Show("Debe escribir una justificación");

                    return;
                }

                _tareaService.ActualizarTarea(tarea);

               
               
                icTareas.Items.Refresh();
                tarea.Justificacion = "";

                MessageBox.Show("Tarea actualizada correctamente");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Estado_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            var tarea = radio.DataContext as Tarea;

            if (tarea != null)
            {
                tarea.ID_estado = int.Parse(radio.Tag.ToString());
            }

        }
        private void VerTareas_click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new TareasTecnicoView();

        }
        private void Cambiarcontra_click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new CamPassView(Sesion.UsuarioId);
        }
        private void Recargar_Click(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
