using AppStockControl.Helpers;
using AppStockControl.Models;

namespace AppStockControl.Views;

public partial class ProductUpdate : ContentPage
{
    private string _selectedImagePath = null;
    public ProductUpdate()
	{
		InitializeComponent();
	}

	public void SetEdit(Product product)
	{
		try
		{
			txtDescription.Text = product.Description;
			txtStockQuantity.Text = product.StockQuantity.ToString();
			txtMinimumStock.Text = product.MinimumStock.ToString();
			txtMaximumStock.Text = product.MaximumStock.ToString();
			txtBuyPrice.Text = product.BuyPrice.ToString("C");
			txtSalePrice.Text = product.SalePrice.ToString("C");
			pkUniType.ItemDisplayBinding.FallbackValue = product.UnitType.ToString();
			txtBarCode.Text = product.Barcode;
			dpCreateAndUpdate.Date = product.CreatedAt.Date;
			ibProduct.Source = product.ImagePath;
			txtObs.Text = product.Observation;
		}
		catch(ArgumentException ae)
		{
			DisplayAlert("Ops!", ae.Message, "Fechar");
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
	}

    private void Button_Clicked_Update(object sender, EventArgs e)
    {

    }

    private async void ImageButton_Clicked_SelectImage_Update(object sender, EventArgs e)
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
}