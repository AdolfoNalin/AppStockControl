using AppStockControl.Models;
using AppStockControl.Service;
using AppStockControl.Views;

namespace AppStockControl
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();
                MainPage = new SplashPage();
                _ = InitializeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task InitializeAsync()
        {
            Guid userId = Guid.Parse(await SecureStorage.GetAsync("userId"));
            bool restored = await AuthService.AuthUser(userId);
            MainPage = restored ? new AppShell() : new Login();
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