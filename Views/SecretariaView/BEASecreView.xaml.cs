using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TDL.Data;
using TDL.Repositories;
using TDL.Services;
using TDL.ViewModel;

namespace TDL
{
    /// <summary>
    /// Lógica de interacción para BEAdminView.xaml
    /// </summary>
    public partial class BEASecreView : UserControl
    {
        private TareaService _tareaService;
        private TareaRepository _tareaRepository;
        public BEASecreView()
        {
            InitializeComponent();
            var context = new AppDbContext();
            var tarearepo = new TareaRepository(context);
            var historialRepo = new HistorialAccionRepository(context);

            var historialService = new HistorialAccionService(historialRepo);



            _tareaService = new TareaService(tarearepo, historialService);

            dgbuscar.ItemsSource = new List<object>();
            CargarTareas();

       

        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {


            string texto = txtbuscar.Text.Trim();

            List<TareaVM> resultados;

            if (string.IsNullOrEmpty(texto))
            {
                CargarTareas();
                return;
            }
            else
            {
               
                resultados = _tareaService.BuscarTareas(texto);
            }

            dgbuscar.ItemsSource = resultados;


        }

        private void txtbuscar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgbuscar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btnvolver_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.MainContent.Content = new SecretariaView();
        }
        private void btnimprimir_Click(object sender, RoutedEventArgs e)
        {
            if (dgbuscar.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione al menos una tarea");
                return;
            }

            var tareas = dgbuscar.SelectedItems.OfType<TareaVM>().ToList();

            using (var workbook = new XLWorkbook())
            {
                var hoja = workbook.Worksheets.Add("Reporte de Tareas");

               
                string rutaLogo = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Images",
                    "bellas artes.png");

                if (File.Exists(rutaLogo))
                {
                    hoja.AddPicture(rutaLogo)
                        .MoveTo(hoja.Cell("A1"))
                        .WithSize(120, 80);
                }

              
                hoja.Range("C2:I2").Merge();

                var titulo = hoja.Cell("C2");

                titulo.Value = "REPORTE DE TAREAS";
                titulo.Style.Font.Bold = true;
                titulo.Style.Font.FontSize = 20;
                titulo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                titulo.Style.Font.FontColor = XLColor.DarkBlue;

                // ===== ENCABEZADOS =====
                string[] encabezados =
                {
                    "Título",
                    "Descripción",
                    "Estado",
                    "Prioridad",
                    "Técnico",
                    "Justificación",
                    "Fecha Apertura",
                    "Fecha Límite",
                    "Duración"
                };

                        for (int i = 0; i < encabezados.Length; i++)
                        {
                            var celda = hoja.Cell(5, i + 1);

                            celda.Value = encabezados[i];

                            celda.Style.Font.Bold = true;
                            celda.Style.Font.FontColor = XLColor.White;
                            celda.Style.Fill.BackgroundColor = XLColor.SteelBlue;

                            celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            celda.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            celda.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }

              
                int fila = 6;

                foreach (var tarea in tareas)
                {
                    hoja.Cell(fila, 1).Value = tarea.Titulo;
                    hoja.Cell(fila, 2).Value = tarea.Descripcion;
                    hoja.Cell(fila, 3).Value = tarea.Estado;
                    hoja.Cell(fila, 4).Value = tarea.Prioridad;
                    hoja.Cell(fila, 5).Value = tarea.Tecnico;
                    hoja.Cell(fila, 6).Value = tarea.Justificacion;
                    hoja.Cell(fila, 7).Value = tarea.Fecha_apertura;
                    hoja.Cell(fila, 8).Value = tarea.Fecha_limite;

                    
                    TimeSpan duracion = tarea.Fecha_limite - tarea.Fecha_apertura;

                    hoja.Cell(fila, 9).Value = $"{duracion.Days} días";

                    var rangoFila = hoja.Range($"A{fila}:I{fila}");

                    rangoFila.Style.Alignment.WrapText = true;
                    rangoFila.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                    rangoFila.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                    rangoFila.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rangoFila.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    hoja.Row(fila).AdjustToContents();

                    fila++;
                }

                hoja.Column(1).Width = 25;
                hoja.Column(2).Width = 35;
                hoja.Column(3).Width = 18;
                hoja.Column(4).Width = 18;
                hoja.Column(5).Width = 25;
                hoja.Column(6).Width = 40;
                hoja.Column(7).Width = 22;
                hoja.Column(8).Width = 22;
                hoja.Column(9).Width = 15;

                hoja.Protect("1234");

                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Reporte_Tareas_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");

                try
                {
                    workbook.SaveAs(ruta);

                    MessageBox.Show("Excel generado correctamente");

                    Process.Start(new ProcessStartInfo(ruta)
                    {
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar Excel: {ex.Message}");
                }
            }

     
        }
        private void CargarTareas()
        {
            dgbuscar.ItemsSource = _tareaService.ObtenerTareas();
        }
        private void btnSeleccionarTodo_Click(object sender, RoutedEventArgs e)
        {
            dgbuscar.SelectAll();
        }

    }
}
