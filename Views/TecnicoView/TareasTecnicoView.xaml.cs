using DocumentFormat.OpenXml.InkML;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TDL.Data;
using TDL.Helpers;
using TDL.Repositories;
using TDL.Services;
using TDL.ViewModel;

namespace TDL
{
    /// <summary>
    /// Lógica de interacción para TareasTecnicoView.xaml
    /// </summary>
    public partial class TareasTecnicoView : UserControl
    {
        private TareaService _tareaService;
        public TareasTecnicoView()
        {
            InitializeComponent();
            var context = new AppDbContext();
            var tarearepo = new TareaRepository(context);
            var historialRepo = new HistorialAccionRepository(context);

            var historialService = new HistorialAccionService(historialRepo);



            _tareaService = new TareaService(tarearepo, historialService);
            CargarTareas();
        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            string texto = txtbuscar.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                dgbuscar.ItemsSource = _tareaService.ObtenerTareasPorTecnicoBuscar(Sesion.UsuarioId, "");
                return;
            }

            dgbuscar.ItemsSource = _tareaService.ObtenerTareasPorTecnicoBuscar(Sesion.UsuarioId, texto);


        }
        private void txtbuscar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void btnvolver_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new TecnicoView(Sesion.UsuarioId);
        }
        private void CargarTareas()
        {
            dgbuscar.ItemsSource = _tareaService.ObtenerTareasPorTecnicoBuscar(Sesion.UsuarioId, "");
        }
    }
}
