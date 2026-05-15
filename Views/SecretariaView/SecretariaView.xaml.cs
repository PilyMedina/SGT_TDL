using System.Windows;
using System.Windows.Controls;
using TDL.Data;
using TDL.Helpers;
using TDL.Repositories;
using TDL.Services;


namespace TDL
{
    /// <summary>
    /// Lógica de interacción para AdminView.xaml
    /// </summary>
    public partial class SecretariaView : UserControl
    {
        private TareaService _tareaService;
        private UsuarioService _usuarioService;
        public SecretariaView()
        {
            InitializeComponent();

            
            var contexto = new AppDbContext();
            var tarearepo = new TareaRepository(contexto);
            _tareaService = new TareaService(tarearepo);

           _tareaService = new TareaService(tarearepo);
            cmbUsuarios.ItemsSource = _tareaService.ObtenerTecnico();
            cmbUsuarios.DisplayMemberPath = "Nombre";
            cmbUsuarios.SelectedValuePath = "ID";


            CargarTareas();


        }

        private void BuscarTarea_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new BEASecreView();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string titulo = txtTitulo.Text;
                string descripcion = txtDescripcion.Text;

                if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
               string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Titulo y Descripcion no pueden estar vacios");
                    return;
                }

                if (cmbUsuarios.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un tecnico");
                    return;

                }

                if (dpFecha.SelectedDate == null)
                {
                    MessageBox.Show("Debe seleccionar una fecha limite");
                    return;
                }

                int idTecnico = Convert.ToInt32(cmbUsuarios.SelectedValue);

               


                int idprioridad = 0;
                if(rbalta.IsChecked == true)
                {
                    idprioridad = 1;
                }
                else if (rbmedia.IsChecked == true)
                {
                    idprioridad = 2;
                }
                else if (rbbaja.IsChecked == true)
                {
                    idprioridad = 3;
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una prioridad");
                    return;
                    
                }
                DateTime fechaLimite = dpFecha.SelectedDate.Value;

                //Falta la fecha
                _tareaService.CrearTarea(
                    titulo,
                    descripcion,
                    idprioridad,
                    idTecnico,
                    fechaLimite);

               
                CargarTareas();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);

            }
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            cmbUsuarios.SelectedIndex = -1;

            dpFecha.SelectedDate = null;

            rbalta.IsChecked = false;
            rbmedia.IsChecked = false;
            rbbaja.IsChecked = false;



        }

        private void CargarTareas()
        {
            dgTareas.ItemsSource = _tareaService.ObtenerTareas();
        }
        private void CerrarSesion_click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new LoginView();
        }
        private void Cambiarcontra_click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new CamPassView(Sesion.UsuarioId);
        }

    }
}
