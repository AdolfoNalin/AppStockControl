using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;

namespace AppStockControl.Views;

public partial class CategoryUpdate : ContentPage
{
	public CategoryUpdate()
	{
		InitializeComponent();
	}

	public void SetEdit(Category category)
	{
		try
		{
			txtNameCategory.Text = category.Name;
			txtDescriptionCategory.Text = category.Description;
			rbEnable.IsChecked = category.Active ? true : false;
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
	}

    #region Button_Cliked_Save
    private async void Button_Cliked_Save(object sender, EventArgs e)
    {
		try
		{
			Category category = new Category()
			{
				Name = txtNameCategory.Text,
				Description = txtDescriptionCategory.Text,
				Active = rbEnable.IsChecked ? true : false,
				UpdatedAt = DateTime.UtcNow,
			};

			string message = await CategoryService.Update(category);

			DisplayAlert("Erro", message, "Fechar");

			WeakReferenceMessenger.Default.Send<String>("Category");

			Navigation.PopAsync();
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion

    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}