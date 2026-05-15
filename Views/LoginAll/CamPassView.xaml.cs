using System.Windows;
using System.Windows.Controls;
using TDL.Data;
using TDL.Helpers;
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
                    // VALIDAR QUE LAS CONTRASEÑAS COINCIDAN
                    if (txtNuevaPassword.Password != txtConfirmarPassword.Password)
                    {
                        MessageBox.Show("Las contraseñas no coinciden");
                        return;
                    }

                    string nuevaContrasena =
                        SeguridadHelper.Encriptar(txtNuevaPassword.Password);

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
