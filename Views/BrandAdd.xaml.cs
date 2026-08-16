using AppStockControl.DTOs;
using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;
namespace AppStockControl.Views;

public partial class BrandAdd : ContentPage
{
	public BrandAdd()
	{
		InitializeComponent();
	}

    #region Button_Clicked_Save
    private async void Button_Clicked_Save(object sender, EventArgs e)
    {
		try
		{
			Brand brand = new Brand()
			{
				UserId = UserSession.Id,
				Name = txtName.Text,
				Description = txtDescription.Text,
				Active = rbEnable.IsChecked ? true : false
			};

			string message = await BrandService.Create(brand);
			
			DisplayAlert("", message, "Fechar");


			WeakReferenceMessenger.Default.Send("Brand");

			Navigation.PopAsync();
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion
}