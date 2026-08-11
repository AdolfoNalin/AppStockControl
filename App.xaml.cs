using AppStockControl.Views;

namespace AppStockControl
{
    public partial class App : Application
    {
        public App(Login login)
        {
            MainPage = login;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Height = 700;
            window.Width = 600;
            return window;
        }
    }
}