using System.Windows;
using System.Windows.Controls;
using TDL.AdminView;
using TDL.Data;
using TDL.Helpers;
using TDL.Repositories;
using TDL.Repositories.Interfaces;
using TDL.Services;
using TDL.Services.Interfaces;
using TDL.Views.LoginView;




namespace TDL
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private ILoginService service;
        private UsuarioService usuarioService;
        public LoginView()
        {
            InitializeComponent();
            var contexto = new AppDbContext();
            var repo = new UsuarioRepository(contexto);
            service = new LoginService(repo);
        }

        

        private void Acceder(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtusuario.Text) ||
                string.IsNullOrWhiteSpace(txtcontra.Password))
                {
                    MessageBox.Show("Todos los campos son obligatorios");
                    return;
                }

                var usuario = service.Login(txtusuario.Text, txtcontra.Password);

                if (usuario == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                    return;
                }

                Sesion.UsuarioId = usuario.ID;
                Sesion.NombreUsuario = usuario.Nombre;

              
                int tecnicoId = usuario.ID;

                var mainWindow = (MainWindow)Window.GetWindow(this);
                if (usuario.ID_rol == 1)
                {
                    
                    mainWindow.MainContent.Content = new SecretariaView();
                }
                else if(usuario.ID_rol == 2)
                {
                   
                    mainWindow.MainContent.Content = new TecnicoView(tecnicoId);
                }
                else if (usuario.ID_rol == 3)
                {

                    mainWindow.MainContent.Content = new AdminLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Olvide(object sender, RoutedEventArgs e)
        {

            var context = new AppDbContext();

            ITokenRepository repository =
                new TokenRepository(context);

            ITokenService service =
                new TokenService(repository);

            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new TokenView(service);
        }


    }
}
