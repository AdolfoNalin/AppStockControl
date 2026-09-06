using AppStockControl.DTOs;
using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AppStockControl.Views;

public partial class ProductAdd : ContentPage
{
    private string _selectedImagePath = null;

    public ProductAdd()
    {
        InitializeComponent();
        UpdateData();
    }

    private async void UpdateData()
    {
        try
        {
            List<String> uniTypeString = new List<String>()
            {
                "Unidade",
                "Kg",
                "Litros",
                "Caixa",
                "Pacote"
            };

            pkUniType.ItemsSource = uniTypeString;
            ObservableCollection<Brand> brands = await BrandService.GetByStatus(true);
            ObservableCollection<Category> categorys = await CategoryService.GetByStatus(true);
            ObservableCollection<Supplier> suppliers = await SupplierService.GetByStatus(true);

            pkCategory.ItemsSource = categorys;
            pkSupplier.ItemsSource = suppliers;
            pkBrand.ItemsSource = brands;
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }

    #region Button_Clicked_Cancel
    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    #endregion

    #region Button_Clicked_Save
    private async void Button_Clicked_Save(object sender, EventArgs e)
    {
        try
        {
            Category category = pkCategory.SelectedItem as Category;
            Brand brand = pkBrand.SelectedItem as Brand;
            Supplier supplier = pkSupplier.SelectedItem as Supplier;

            Product product = new Product()
            {
                BrandId = brand.Id,
                UserId = UserSession.Id,
                SupplierId = supplier.Id,
                CategoryId = category.Id,
                Description = txtDescription.Text,
                StockQuantity = int.Parse(txtStockQuantity.Text),
                MaximumStock = int.Parse(txtMaximumStock.Text),
                MinimumStock = int.Parse(txtMinimumStock.Text),
                BuyPrice = Decimal.Parse(txtBuyPrice.Text),
                SalePrice = Decimal.Parse(txtSalePrice.Text),
                CreatedAt = DateOnly.FromDateTime(dpCreateAndUpdate.Date.Value),
                Observation = txtObs.Text,
                ImagePath = _selectedImagePath,
                UnitType = (UnitType)pkUniType.SelectedIndex
            };

            string message = await ProductService.Create(product);

            DisplayAlert("Erro", message, "Fechar");

            WeakReferenceMessenger.Default.Send<String>("Product");

            Navigation.PopAsync();
        }
        catch (ArgumentNullException ane)
        {
            DisplayAlert("Erro", ane.ParamName, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }   
    #endregion

    #region ImageButton_Clicked_SelectImage
    private async void ImageButton_Clicked_SelectImage(object sender, EventArgs e)
    {
        try
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();

            string localPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.Open(localPath, FileMode.Create, FileAccess.Write);
            await sourceStream.CopyToAsync(localFileStream);

            ibProduct.Source = ImageSource.FromFile(localPath);

            _selectedImagePath = localPath;
        }
        catch (NullReferenceException nre)
        {
            DisplayAlert("Selecione uma imagem", "Nenhuma imagem foi selecionada", "Fechar");
        }
        catch (FeatureNotSupportedException fnse)
        {
            DisplayAlert("Formato da Imagem", "Formato da imagem não coerente", "Fechar");
        }
        catch (PermissionException pe)
        {
            DisplayAlert("Permissão negada", "Permita que o App acesse a galeria", "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fecha");
        }
    }
    #endregion

    private void ImageButton_Back(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}