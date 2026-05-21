using System;
using System.Windows;
using System.Windows.Controls;
using TDL.Helpers;
using TDL.Views.Historial;

namespace TDL.AdminView
{
    public partial class AdminOpciones : UserControl
    {
        public event Action<UserControl>? CambiarVista;

        public AdminOpciones()
        {
            InitializeComponent();
        }

        private void AsignarTarea_Click(object sender, RoutedEventArgs e)
        {
            CambiarVista?.Invoke(new AdminAggView());
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {

            CambiarVista?.Invoke(new BEAdminView());
        }
        private void EditarTarea_Click(object sender, RoutedEventArgs e)
        {
            CambiarVista?.Invoke(new EditarTarea());
        }
        private void Historial_Click(object sender, RoutedEventArgs e)
        {
            CambiarVista?.Invoke(new HistorialTotalView());
        }

        private void AgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            CambiarVista?.Invoke(new AgregarUsuario());
        }

        private void EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            CambiarVista?.Invoke(new EliminarUsuario());
        }

        private void CambiarPassword_Click(object sender, RoutedEventArgs e)
        {
            CambiarVista?.Invoke(new CamPassView(Sesion.UsuarioId));
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new LoginView();
        }
    }
}