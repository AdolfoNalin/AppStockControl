using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AppStockControl.Views;

public partial class SupplierList : ContentPage
{
    public SupplierList()
    {
        InitializeComponent();

        UpdateData();

        WeakReferenceMessenger.Default.Register<String>("Supplier", (e, message) =>
        {
            UpdateData();
        });
    }

    private async void UpdateData()
    {
        try
        {
            ObservableCollection<Supplier> suppliers = await SupplierService.GetAll();
            cvCategory.ItemsSource = suppliers;
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

    private void Button_Clicked_ScreenInsertSupplierAdd(object sender, EventArgs e)
    {
        SupplierAdd add = this.Handler.MauiContext.Services.GetService<SupplierAdd>();
        Navigation.PushAsync(add);

    }

    private void Button_Clicked_ScreenUpdate(object sender, EventArgs e)
    {
        try
        {
            SwipeItem item = sender as SwipeItem;
            Supplier supplier = item.CommandParameter as Supplier;
            SupplierUpdate update = this.Handler.MauiContext.Services.GetService<SupplierUpdate>();
            update.SetEdit(supplier);
            Navigation.PushAsync(update);
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }

    private void Button_ChangeStatus(object sender, EventArgs e)
    {

    }
}