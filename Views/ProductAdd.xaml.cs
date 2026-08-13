using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;

namespace AppStockControl.Views;

public partial class ProductAdd : ContentPage
{
    private string _selectedImagePath = null;
    public ProductAdd()
    {
        InitializeComponent();
        Task.Run(async () =>
        {
            pkCategory.ItemsSource = await CategoryService.GetAll();
            pkBrand.ItemsSource = await BrandService.GetAll();
        });
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
                Observation = txtObs.Text,
                ImagePath = _selectedImagePath,
                UnitType = (UnitType)int.Parse(pkUniType.Id.ToString())
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
}