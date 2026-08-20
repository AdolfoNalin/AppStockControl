using AppStockControl.DTOs;
using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;

namespace AppStockControl.Views;

public partial class CategoryAdd : ContentPage
{
	public CategoryAdd()
	{
		InitializeComponent();

	}

    #region Button_Cliked_Save
    private async void Button_Cliked_Save(object sender, EventArgs e)
    {
		try
		{
			Category category = new Category()
			{
				UserId = UserSession.Id,
				Name = txtNameCategory.Text,
				Description = txtDescriptionCategory.Text,
				Active = rbEnable.IsChecked ? true : false
			};

			string message = await CategoryService.Create(category);

			DisplayAlert("", message, "Fechar");

			WeakReferenceMessenger.Default.Send<String>("Category");

			Navigation.PopAsync();
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion
}