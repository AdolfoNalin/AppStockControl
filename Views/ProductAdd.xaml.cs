using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;

namespace AppStockControl.Views;

public partial class ProductAdd : ContentPage
{
	public ProductAdd()
	{
		InitializeComponent();
		Task.Run(async () =>
		{
			pkCategory.ItemsSource = await CategoryService.GetAll();
			pkBrand.ItemsSource = await BrandService.GetAll();
		});
	}

    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }

    #region Button_Clicked_Save
    private async void Button_Clicked_Save(object sender, EventArgs e)
    {
		try
		{
			Product product = new Product()
			{
				CategoryId = Guid.Parse(pkCategory.Id.ToString()),
				BrandId = Guid.Parse(pkBrand.Id.ToString()),
				Description = txtDescription.Text,
				StockQuantity = int.Parse(txtStockQuantity.Text),
				MaximumStock = int.Parse(txtMaximumStock.Text),
				MinimumStock = int.Parse(txtMinimumStock.Text),
				BuyPrice = Decimal.Parse(txtBuyPrice.Text),
				SalePrice = Decimal.Parse(txtSalePrice.Text),
				CreatedAt = dpCreateAndUpdate.Date.Value,
				Observation = txtObs.Text
				//UnitType = int.Parse(pkUniType.Id.ToString())
			};

			await ProductService.Create(product);
		}
		catch (ArgumentNullException ane)
		{
			DisplayAlert("Erro", ane.ParamName, "Fechar");
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
			throw;
		}
    }
    #endregion
}