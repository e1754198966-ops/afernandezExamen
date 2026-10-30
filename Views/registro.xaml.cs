namespace afernandezExamen.Views
{
    public partial class registro : ContentPage
    {
        private readonly string usuarioActual;
        private const double COSTO_TOTAL = 1200.0;
        private double pagoMensualCalculado = 0;
        private bool esPagoCalculado = false;

        public registro(string usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            lblUsuarioConectado.Text = $"Usuario conectado: {usuarioActual}";

            dpFechaInicio.MinimumDate = DateTime.Today;
            dpFechaInicio.Date = DateTime.Today;
        }

        private bool ValidarCampos(out int edad, out double montoInicial)
        {
            edad = 0;
            montoInicial = 0;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                DisplayAlert("Error", "Debe ingresar Nombre y Apellido.", "OK");
                return false;
            }

            if (!int.TryParse(txtEdad.Text, out edad) || edad < 16)
            {
                DisplayAlert("Error", "La edad debe ser un número entero mayor o igual a 16.", "OK");
                return false;
            }

            if (pickerPlan.SelectedIndex == -1)
            {
                DisplayAlert("Error", "Debe seleccionar un Plan.", "OK");
                return false;
            }

            if (!double.TryParse(txtMontoInicial.Text, out montoInicial) || montoInicial < 120 || montoInicial > 600)
            {
                DisplayAlert("Error", "El Monto Inicial debe estar entre 120 (10%) y 600 (50%).", "OK");
                return false;
            }

            return true;
        }

        private void OnCalcularPagoClicked(object sender, EventArgs e)
        {
            if (!ValidarCampos(out _, out double montoInicial))
                return;

            double saldoRestante = COSTO_TOTAL - montoInicial;
            double baseCuota = saldoRestante / 5.0;
            double recargoPorcentual = COSTO_TOTAL * 0.02;
            double adicionalCasillero = pickerCasillero.SelectedItem?.ToString() == "Sí" ? 10.0 : 0.0;

            pagoMensualCalculado = baseCuota + recargoPorcentual + adicionalCasillero;
            txtPagoMensual.Text = pagoMensualCalculado.ToString("F2");
            esPagoCalculado = true;
        }

        private void OnVerResumenClicked(object sender, EventArgs e)
        {
            if (!ValidarCampos(out int edad, out double montoInicial))
                return;

            if (!esPagoCalculado)
            {
                DisplayAlert("Atención", "Primero debe calcular el pago mensual.", "OK");
                return;
            }

            double pagoTotal = montoInicial + (pagoMensualCalculado * 5);
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string fechaInicio = dpFechaInicio.Date.Value.ToString("dd/MM/yyyy"); ;
            string plan = pickerPlan.SelectedItem.ToString() ?? string.Empty;
            string casilleroStr = pickerCasillero.SelectedItem?.ToString() == "Sí" ? "Sí" : "No";

            // Pasamos los valores directamente como parámetros a la ventana resumen sin usar una clase de modelo
            Navigation.PushAsync(new resumen(usuarioActual, nombre, apellido, edad, fechaInicio, plan, casilleroStr, montoInicial, pagoMensualCalculado, pagoTotal));
        }
    }
}