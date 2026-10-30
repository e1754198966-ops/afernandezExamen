namespace afernandezExamen.Views
{
    public partial class login : ContentPage
    {
        // Creación de la matriz 4x2 con los usuarios y contraseñas solicitados
        private readonly string[,] usuariosMatriz = new string[4, 2]
        {
            { "recepcion", "gym2026" },
            { "entrenador", "fit_01" },
            { "uisrael", "2026" },
            { "gerente", "admin123" }
        };

        public login()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string usuarioInput = txtUsuario.Text?.Trim() ?? string.Empty;
            string passwordInput = txtPassword.Text ?? string.Empty;

            bool credencialesValidas = false;

            // Recorremos la matriz para validar las credenciales ingresadas
            for (int i = 0; i < 4; i++)
            {
                if (usuariosMatriz[i, 0] == usuarioInput && usuariosMatriz[i, 1] == passwordInput)
                {
                    credencialesValidas = true;
                    break;
                }
            }

            if (credencialesValidas)
            {
                txtPassword.Text = string.Empty;
                Navigation.PushAsync(new registro(usuarioInput));
            }
            else
            {
                DisplayAlert("Error", "Dato incorrecto", "OK");
            }
        }

        private void OnAcercaDeClicked(object sender, EventArgs e)
        {
            DisplayAlert("Acerca de", "Nombre: Anthony Fernández\nCarrera: Ingeniería en Sistemas\nParalelo: A", "OK");
        }
    }
}