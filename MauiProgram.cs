using AppStockControl.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppStockControl
{
    public static class MauiProgram
    {
        public static string endPointAPI = "";
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).RegisterViews();

#if DEBUG
    		builder.Logging.AddDebug();
#endif



            using var stream = FileSystem.OpenAppPackageFileAsync("appsettings.json").GetAwaiter().GetResult();

            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            builder.Configuration.AddConfiguration(config);

            endPointAPI = config["StockControlApp:Endpoint"];

            return builder.Build();
        }

        #region RegisterViews
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            try
            {
                mauiAppBuilder.Services.AddTransient<Supplier>();
                mauiAppBuilder.Services.AddTransient<BrandList>();
                mauiAppBuilder.Services.AddTransient<BrandAdd>();
                mauiAppBuilder.Services.AddTransient<CategoryAdd>();
                mauiAppBuilder.Services.AddTransient<CategoryList>();
                mauiAppBuilder.Services.AddTransient<Login>();
                mauiAppBuilder.Services.AddTransient<SignUp>();
                mauiAppBuilder.Services.AddTransient<Menu>();
                mauiAppBuilder.Services.AddTransient<ProductAdd>();
                mauiAppBuilder.Services.AddTransient<ProductList>();
                mauiAppBuilder.Services.AddTransient<AppShell>();
                mauiAppBuilder.Services.AddTransient<App>();

                return mauiAppBuilder;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
