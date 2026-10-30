using afernandezExamen.Views;

namespace afernandezExamen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new login());
        }
    }
}