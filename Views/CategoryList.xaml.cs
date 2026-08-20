using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

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

    #region Button_Clicked_ScreenInsert
    private void Button_Clicked_ScreenInsert(object sender, EventArgs e)
    {
        CategoryAdd add = this.Handler.MauiContext.Services.GetService<CategoryAdd>();
        Navigation.PushAsync(add);
    }
    #endregion

    #region Button_Clicked_ScreenUpdate
    private void Button_Clicked_ScreenUpdate(object sender, EventArgs e)
    {
        try
        {
            SwipeItem swipeItem = sender as SwipeItem;
            Category category = swipeItem.CommandParameter as Category ?? throw new ArgumentNullException("Categoria não encontrada");

            CategoryUpdate update = this.Handler.MauiContext.Services.GetService<CategoryUpdate>();
            update.SetEdit(category);
            Navigation.PushAsync(update);
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("Ops!", ane.ParamName, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region Button_ChangeStatus
    private async void Button_ChangeStatus(object sender, EventArgs e)
    {
        try
        {
            SwipeItem item = sender as SwipeItem;
            Category category = item.CommandParameter as Category;
            string value = category.Active ? "Desativar" : "Ativar";
            bool result = await DisplayAlert("Inativação", $"Deseja {value} essa categoria", "Sim", "Não");

            if (result)
            {
                string message = await CategoryService.ChangeSatus(category.Id);
                DisplayAlert("Ops!", message, "Fechar");

                WeakReferenceMessenger.Default.Send<String>("Categpry");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
            throw;
        }
    }
    #endregion
}