using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AppStockControl.Views;

public partial class BrandList : ContentPage
{
	public BrandList()
	{
		InitializeComponent();
		UpdateData();
		WeakReferenceMessenger.Default.Register<String>("Brand", (e, message) =>
		{
			UpdateData();
		});
	}

    #region UpdateData
    private async void UpdateData()
	{
		try
		{
			ObservableCollection<Brand> brands = await BrandService.GetAll();
			cvBrand.ItemsSource = brands;
		}
		catch(ArgumentNullException ane)
		{
			DisplayAlert("Erro", ane.ParamName, "Fechar");
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
	}
    #endregion

    #region Button_Clicked_ScreenInsert
    private void Button_Clicked_ScreenInsert(object sender, EventArgs e)
    {
		try
		{
			BrandAdd brand = this.Handler.MauiContext.Services.GetService<BrandAdd>();
			Navigation.PushAsync(brand);
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion
}