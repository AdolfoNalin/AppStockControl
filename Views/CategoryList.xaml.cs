using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AppStockControl.Views;

public partial class CategoryList : ContentPage
{
	public CategoryList()
	{
		InitializeComponent();

        UpdateData();
        WeakReferenceMessenger.Default.Register<string>("Category", (e, message) =>
        {
            UpdateData();
        });
	}

    #region UpdateData
    private async void UpdateData()
    {
        try
        {
            ObservableCollection<Category> categorys = await CategoryService.GetAll();
            cvCategory.ItemsSource = categorys;
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("", ane.ParamName, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    #region
    private void Button_Clicked_ScreenInsert(object sender, EventArgs e)
    {
        CategoryAdd add = this.Handler.MauiContext.Services.GetService<CategoryAdd>();
        Navigation.PushAsync(add);
    }
    #endregion
}