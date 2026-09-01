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

        dpStartDate.IsEnabled = false;
        dpEndDate.IsEnabled = false;
    }

    #region UpdateData
    private async void UpdateData()
    {
        try
        {
            ObservableCollection<Category> categories = await CategoryService.GetByStatus(true);
            pkCategory.ItemsSource = categories;
            ObservableCollection<Product> products = await ProductService.GetAll();
            cvProduct.ItemsSource = products;
        }
        catch (ArgumentNullException ane)
        {
            DisplayAlert("Erro", ane.ParamName, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region Button_Clicked_AddProduct
    private void Button_Clicked_AddProduct(object sender, EventArgs e)
    {
        try
        {
            ProductAdd product = this.Handler.MauiContext.Services.GetService<ProductAdd>();
            Navigation.PushAsync(product);
        }
        catch (ArgumentException ae)
        {
            DisplayAlert("Erro", ae.Message, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region OnOpenCamareClicked
    private async void OnOpenCameraClicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync()
                    ?? throw new NullReferenceException();

                if (photo != null)
                {
                    var localPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localStream = File.OpenWrite(localPath);
                    await sourceStream.CopyToAsync(localStream);


                }
            }
        }
        catch (NullReferenceException nre)
        {
            DisplayAlert("", "Nenhum imagem foi encontrada", "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region Button_Clciked_ScreenUpdate
    private void Button_Clciked_ScreenUpdate(object sender, EventArgs e)
    {
        try
        {
            SwipeItem item = sender as SwipeItem;
            Product product = item.CommandParameter as Product;

            ProductUpdate update = this.Handler.MauiContext.Services.GetService<ProductUpdate>();
            update.SetEdit(product);

            Navigation.PushAsync(update);

        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region Button_Clicked_ChangeStatus
    private async void Button_Clicked_ChangeStatus(object sender, EventArgs e)
    {
        try
        {
            SwipeItem item = sender as SwipeItem;
            Product product = item.CommandParameter as Product;

            string message = await ProductService.ChangeStatus(product.Id);

            DisplayAlert("", message, "Fechar");

            WeakReferenceMessenger.Default.Send<String>("Product");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region txtSearch_TextChange
    private async void txtSearch_TextChange(object sender, TextChangedEventArgs e)
    {
        try
        {
            string value = txtSeach.Text;
            ObservableCollection<Product> products = await ProductService.GetSmart(value);
            cvProduct.ItemsSource = products;
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region Picker_SelectedIndexChanged_Category
    private async void Picker_SelectedIndexChanged_Category(object sender, EventArgs e)
    {
        try
        {
            if (pkCategory.SelectedItem.Equals("Nenhum"))
            {
                UpdateData();
            }
            else
            {
                Category category = pkCategory.SelectedItem as Category;
                ObservableCollection<Product> products = await ProductService.GetAll();
                cvProduct.ItemsSource = products.Where(p => p.CategoryId == category.Id).OrderBy(p => p.Description);
            }
        }
        catch (ArgumentNullException ane)
        {
            DisplayAlert("Erro", ane.ParamName, "Fechar");
        }
        catch (ArgumentException ae)
        {
            DisplayAlert("Erro", ae.Message, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region CheckBox_CheckedChanged_EnableDate
    private void CheckBox_CheckedChanged_EnableDate(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            if (cbIsDateFilter.IsChecked)
            {
                dpStartDate.IsEnabled = true;
                dpEndDate.IsEnabled = true;
            }
            else
            {
                dpStartDate.IsEnabled = false;
                dpEndDate.IsEnabled = false;
                UpdateData();
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }
    #endregion

    #region DatePicker_DataSelected_SeachDate
    private async void DatePicker_DataSelected_SeachDate(object sender, DateChangedEventArgs e)
    {
        try
        {
            DateOnly startDate = DateOnly.FromDateTime(dpStartDate.Date.Value.Date);
            DateOnly endDate = DateOnly.FromDateTime(dpEndDate.Date.Value.Date);
            
            if(endDate <= startDate)
            {
                throw new ArgumentException("A data fim é menor do que a data de inicio");
            }
            else
            {
                string start = startDate.ToString("yyyy-MM-dd");
                string end = endDate.ToString("yyyy-MM-dd");

                ObservableCollection<Product> products = await ProductService.GetByDate(start, end);
                cvProduct.ItemsSource = products;
            }
        }
        catch(ArgumentNullException ane)
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
}