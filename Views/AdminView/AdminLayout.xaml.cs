using System.Windows.Controls;
namespace TDL.AdminView
{
    /// <summary>
    /// Lógica de interacción para AdminLayout.xaml
    /// </summary>
    public partial class AdminLayout : UserControl
    {
        public AdminLayout()
        {
            InitializeComponent();
            MenuAdmin.CambiarVista += MostrarVista;

            
            AdminContent.Content = new AdminAggView();
        }
        private void MostrarVista(UserControl vista)
        {
            AdminContent.Content = vista;
        }
    }

}
