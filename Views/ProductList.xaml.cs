using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AppStockControl.Views;

public partial class ProductList : ContentPage
{
	public ProductList()
	{
		InitializeComponent();

		UpdateData();

		WeakReferenceMessenger.Default.Register<String>("Product", async (e, message) =>
		{
			UpdateData();
		});
	}

	private async void UpdateData()
	{
		try
		{
            ObservableCollection<Product> products = await ProductService.GetAll();
            cvProduct.ItemsSource = products;
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

    #region Button_Clicked_AddProduct
    private void Button_Clicked_AddProduct(object sender, EventArgs e)
    {
		try
		{
			ProductAdd product = this.Handler.MauiContext.Services.GetService<ProductAdd>();
			Navigation.PushAsync(product);
		}
		catch(ArgumentException ae)
		{
			DisplayAlert("Erro", ae.Message, "Fechar");
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion
}