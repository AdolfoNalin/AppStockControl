using AppStockControl.DTOs;
using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;

namespace AppStockControl.Views;

public partial class BrandUpdate : ContentPage
{
	private Guid _brandId;
	public BrandUpdate()
	{
		InitializeComponent();
	}

	public void SetEdit(Brand brand)
	{
		try
		{
			_brandId = brand.Id;
			txtNameCategory.Text = brand.Name;
			txtDescriptionCategory.Text = brand.Description;
			rbEnable.IsChecked = brand.Active ? true : false;
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
	}

    #region Button_Clicked_Cancel
    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
		try
		{
			Navigation.PopAsync();
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion

    #region Button_Cliked_Save
    private async void Button_Cliked_Save(object sender, EventArgs e)
    {
		try
		{
			Brand brand = new Brand()
			{
				Id = _brandId,
				UserId = UserSession.Id,
				Name = txtNameCategory.Text,
				Description = txtDescriptionCategory.Text,
				UpdatedAt = DateTime.UtcNow,
			};

			string message = await BrandService.Update(brand);

			DisplayAlert("Erro", message, "Fechar");

			WeakReferenceMessenger.Default.Send<string>("Brand");

			Button_Clicked_Cancel(sender, e);
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion
}