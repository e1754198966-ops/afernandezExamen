namespace afernandezExamen.Views
{
    public partial class resumen : ContentPage
    {
        private readonly string _nombre;
        private readonly string _apellido;
        private readonly int _edad;
        private readonly string _fechaInicio;
        private readonly string _plan;
        private readonly string _casillero;
        private readonly double _montoInicial;
        private readonly double _pagoMensual;
        private readonly double _pagoTotal;

        public resumen(string usuario, string nombre, string apellido, int edad, string fechaInicio, string plan, string casillero, double montoInicial, double pagoMensual, double pagoTotal)
        {
            InitializeComponent();
            lblUsuarioConectado.Text = $"Usuario conectado: {usuario}";

            _nombre = nombre;
            _apellido = apellido;
            _edad = edad;
            _fechaInicio = fechaInicio;
            _plan = plan;
            _casillero = casillero;
            _montoInicial = montoInicial;
            _pagoMensual = pagoMensual;
            _pagoTotal = pagoTotal;

            CargarDatos();
        }

        private void CargarDatos()
        {
            lblNombre.Text = _nombre;
            lblApellido.Text = _apellido;
            lblEdad.Text = _edad.ToString();
            lblFechaInicio.Text = _fechaInicio;
            lblPlan.Text = _plan;
            lblCasillero.Text = _casillero;
            lblMontoInicial.Text = $"${_montoInicial:F2}";
            lblPagoMensual.Text = $"${_pagoMensual:F2}";
            lblPagoTotal.Text = $"${_pagoTotal:F2}";
        }

        private void OnExportarTxtClicked(object sender, EventArgs e)
        {
            try
            {
                string contenido = $"Nombre: {_nombre}\n" +
                                   $"Apellido: {_apellido}\n" +
                                   $"Edad: {_edad}\n" +
                                   $"Fecha de Inicio: {_fechaInicio}\n" +
                                   $"Plan: {_plan}\n" +
                                   $"Casillero: {_casillero}\n" +
                                   $"Monto Inicial: ${_montoInicial:F2}\n" +
                                   $"Pago Mensual: ${_pagoMensual:F2}\n" +
                                   $"Pago Total: ${_pagoTotal:F2}\n";

                string fileName = $"Socio_{_nombre}_{_apellido}.txt";
                string targetPath = Path.Combine(FileSystem.CacheDirectory, fileName);

                File.WriteAllText(targetPath, contenido);

                DisplayAlert("Éxito", $"Archivo exportado en:\n{targetPath}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudo generar el archivo: {ex.Message}", "OK");
            }
        }

        private void OnCerrarSesionClicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}