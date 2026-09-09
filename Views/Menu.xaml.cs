using AppStockControl.DTOs;
using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using System.Collections.ObjectModel;

namespace AppStockControl.Views;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
		lblHelloUser.Text = $"Olá, {UserSession.Login}!";
		UpdateData();
	}

    #region UpdateData
    private async void UpdateData()
	{
		try
		{
			ObservableCollection<Product> products = await ProductService.GetByStatus(true);
			int quantityTotalStock = 0;
			int value = 0;
			decimal totalPrice = 0;

			products.ToList().ForEach(p => 
			{ 
				quantityTotalStock += p.StockQuantity;
				totalPrice += p.StockQuantity * p.SalePrice;
                if (p.StockQuantity <= p.MinimumStock)
                {
                    value++;
                }
            });

			lblStockQuantity.Text = quantityTotalStock.ToString();
			lblTotalProduct.Text = products.Count().ToString();
			lblMinimumStock.Text = value.ToString();
			lblTotalPrice.Text = totalPrice.ToString("C");
		}
		catch (ArgumentNullException ane)
		{
			DisplayAlert("Erro", ane.ParamName, "Fechar");
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

    #region OnClickedBorder_Product
    private async void OnClickedBorder_Product(object sender, TappedEventArgs e)
    {
		try
		{
			await Shell.Current.GoToAsync("//product");
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion

    #region OnClickedBorder_Category
    private async void OnClickedBorder_Category(object sender, TappedEventArgs e)
    {
		try
		{
			await Shell.Current.GoToAsync("//category");
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion

    #region OnClickedBorder_Suppliers
    private async void OnClickedBorder_Supplier(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("//supplier");
    }
    #endregion

    #region OnClickedBorder_MovimentStocks
    private void OnClickedBorder_MovimentStock(object sender, TappedEventArgs e)
    {
		MovimentStock stock = this.Handler.MauiContext.Services.GetService<MovimentStock>();
		Navigation.PushAsync(stock);
    }
    #endregion
}