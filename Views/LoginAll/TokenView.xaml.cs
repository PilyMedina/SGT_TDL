
using System.Windows;
using System.Windows.Controls;
using TDL.Data;
using TDL.Repositories;
using TDL.Services.Interfaces;


namespace TDL.Views.LoginView
{
    /// <summary>
    /// Lógica de interacción para TokenView.xaml
    /// </summary>
    public partial class TokenView : UserControl
    {
        private readonly ITokenService _tokenService;
        public TokenView(ITokenService tokenService)
        {
            InitializeComponent();
            _tokenService = tokenService;
        }
        private async void EnviarToken_Click( object sender,RoutedEventArgs e)
        {
            bool enviado =
                await _tokenService
                .EnviarTokenAsync(
                    txtUsuario.Text);

            if (enviado)
            {
                MessageBox.Show(
                    "Token enviado");
            }
            else
            {
                MessageBox.Show(
                    "Usuario no encontrado");
            }
        }

        private void ValidarToken_Click(object sender,RoutedEventArgs e)
        {
            bool valido =
                _tokenService
                .ValidarToken(
                    txtUsuario.Text,
                    txtToken.Text);

            if (valido)
            {
                MessageBox.Show(
                    "Token válido");

                var context =
                    new AppDbContext();

                var repository =
                    new TokenRepository(context);

                var usuario =
                    repository.ObtenerUsuarioPorUsername(
                        txtUsuario.Text);

                var mainWindow =
                    (MainWindow)Window.GetWindow(this);

                mainWindow.MainContent.Content =
                    new CamPassView(usuario.ID);
            }
            else
            {
                MessageBox.Show(
                    "Token inválido o expirado");
            }
        }
        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            //var mainWindow = (MainWindow)Window.GetWindow(this);
            //mainWindow.MainContent.Content = new LoginView();
        }

    }
}
