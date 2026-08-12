using AppStockControl.Views;

namespace AppStockControl
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Task.Delay(800);
            Dispatcher.Dispatch(async () =>
            {
                await Shell.Current.GoToAsync("//menu");
            });
        }
    }
}
 