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
using TDL.Data;
using TDL.Repositories;
using TDL.Services;

namespace TDL.Views.Historial
{
    /// <summary>
    /// Lógica de interacción para HistorialTotalView.xaml
    /// </summary>
    public partial class HistorialTotalView : UserControl
    {
        public event Action<UserControl>? CambiarVista;
        private readonly HistorialAccionService _historialService;
        public HistorialTotalView()
        {
            InitializeComponent();
            var context = new AppDbContext();

            var historialRepo =
                new HistorialAccionRepository(context);

            _historialService =
                new HistorialAccionService(historialRepo);

            CargarHistorial();
        }
        private void CargarHistorial()
        {
            dgHistorial.ItemsSource =
                _historialService.ObtenerHistorial();
        }

    }
}
