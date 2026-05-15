using System.Windows;
using System.Windows.Controls;
using TDL.AdminView;
using TDL.Data;
using TDL.Repositories;
using TDL.Services;

namespace TDL
{
    /// <summary>
    /// Lógica de interacción para AgregarUsuario.xaml
    /// </summary>
    public partial class AgregarUsuario : UserControl
    {
        private readonly UsuarioService _usuarioService;
        public AgregarUsuario()
        {
            InitializeComponent();
            var context = new AppDbContext();

            var repo = new UsuarioRepository(context);

            _usuarioService = new UsuarioService(repo);

        }
       

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idRol = 0;

                // ROLES

                if (rbadmin.IsChecked == true)
                {
                    idRol = 3;
                }
                else if (rbtecnico.IsChecked == true)
                {
                    idRol = 2;
                }
                else if (rbsecretaria.IsChecked == true)
                {
                    idRol = 1;
                }

                // GUARDAR

                _usuarioService.AgregarUsuario(
                    txtnombre.Text,
                    txtapellido.Text,
                    txtpassword.Text,
                    idRol);

                MessageBox.Show("Usuario agregado correctamente");

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
       ex.InnerException?.Message ?? ex.Message);
            }
        }
        private void LimpiarCampos()
        {
            txtnombre.Text="";

            txtapellido.Text = "";

            txtpassword.Text = "";

            rbadmin.IsChecked = false;

            rbtecnico.IsChecked = false;

            rbsecretaria.IsChecked = false;
        }
    }
}
