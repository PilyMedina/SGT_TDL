using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TDL.AdminView;
using TDL.Data;
using TDL.Repositories;
using TDL.Services;

namespace TDL
{
    /// <summary>
    /// Lógica de interacción para EliminarUsuario.xaml
    /// </summary>
    public partial class EliminarUsuario : UserControl
    {
        private readonly UsuarioService _usuarioService;

        public EliminarUsuario()
        {
            InitializeComponent();
            var context = new AppDbContext();
            var repo = new UsuarioRepository(context);
            _usuarioService = new UsuarioService(repo);

        }

        private void btnEliminar(object sender, RoutedEventArgs e)
        {
        
            try
            {
                string usuario = txtusuario.Text;

                MessageBoxResult resultado = MessageBox.Show(
                    $"¿Está seguro que desea borrar al usuario '{usuario}'?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    _usuarioService.EliminarUsuario(usuario);

                    MessageBox.Show("Usuario eliminado correctamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            
            }

        }
        
    }
}
