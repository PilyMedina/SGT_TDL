using System.Windows;
using System.Windows.Controls;
using TDL.Data;
using TDL.Helpers;
using System.Text.RegularExpressions;
using TDL.Repositories;
using TDL.Services;

namespace TDL
{
    /// <summary>
    /// Lógica de interacción para CamPassView.xaml
    /// </summary>
    public partial class CamPassView : UserControl
    {
        private readonly int _idUsuario;
        public CamPassView(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
        }

        private void btncambiar_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                int idUsuario = _idUsuario;

                var usuario = context.Usuarios
                    .FirstOrDefault(u => u.ID == idUsuario);

                if (usuario != null)
                {
                    // VALIDAR QUE COINCIDAN
                    if (txtNuevaPassword.Password != txtConfirmarPassword.Password)
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                        return;
                    }

                    string password = txtNuevaPassword.Password;

                    // VALIDAR SEGURIDAD
                    bool valida =
                        password.Length >= 8 &&
                        Regex.IsMatch(password, "[A-Z]") &&      // Mayúscula
                        Regex.IsMatch(password, "[a-z]") &&      // Minúscula
                        Regex.IsMatch(password, "[0-9]") &&      // Número
                        Regex.IsMatch(password, "[^a-zA-Z0-9]"); // Caracter especial

                    if (!valida)
                    {
                        MessageBox.Show(
                            "La contraseña debe tener mínimo 8 caracteres, " +
                            "una mayúscula, una minúscula, un número y un carácter especial.");
                        return;
                    }

                    string nuevaContrasena =
                        SeguridadHelper.Encriptar(password);

                    usuario.Passsword = nuevaContrasena;

                    context.SaveChanges();

                    MessageBox.Show("Contraseña actualizada");
                }
            }
        }
        private void btnvolver_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new LoginView();
        }
    }
}
