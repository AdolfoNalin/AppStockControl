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
                InitializeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void InitializeAsync()
        {
            try
            {
                Guid userId = Guid.Parse(await SecureStorage.GetAsync("userId"));
                bool restored = await AuthService.AuthUser(userId);
                MainPage = restored ? new AppShell() : new Login();
            }
            catch(ArgumentNullException ane)
            {
                throw ane;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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