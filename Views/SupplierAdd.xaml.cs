using AppStockControl.DTOs;
using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;

namespace AppStockControl.Views;

public partial class SupplierAdd : ContentPage
{
	public SupplierAdd()
	{
		InitializeComponent();
	}

    #region Button_Clicked_Save
    private async void Button_Clicked_Save(object sender, EventArgs e)
    {
		try
		{
			Supplier supplier = new Supplier()
			{ 
				UserId = UserSession.Id,
				Name = txtName.Text,
				TrandName = txtTrandName.Text,
				StateRegistration = txtStateRegistration.Text,
				Cnpj = txtCnpj.Text,
				Email = txtEmail.Text,
				CreatedAt = dpDate.Date.Value.Date.ToUniversalTime(),
				CellPhone = txtCellPhone.Text,
				Phone = txtPhone.Text,
				ContactName = txtConteactName.Text,
				Active = rbEneble.IsChecked ? true : false,
				Observation = txtObservation.Text
			};

			supplier.Address = new Address()
			{
				Id = supplier.Id,
				ZipCode = txtZipCode.Text,
				State = pkUf.Title,
				City = txtCity.Text,
				District = txtDistrict.Text,
				Street = txtStreet.Text,
				Number = txtNumber.Text,
				Complement = txtComplement.Text,
			};

            string message = await SupplierService.Create(supplier);

			WeakReferenceMessenger.Default.Send<String>("Supplier");

			DisplayAlert("", message, "Fechar");

			Navigation.PopAsync();
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion

    #region OnEntryCompleted
    private void OnEntryCompleted(object sender, EventArgs e)
    {
		try
		{
			Address address = ValidationCEP.GetCEP(txtZipCode.Text);

			pkUf.SelectedItem = address.State;
			txtCity.Text = address.City;
			txtDistrict.Text = address.District;
			txtStreet.Text = address.Street;

			txtNumber.Focus();
		}
		catch(ArgumentNullException ane)
		{
			DisplayAlert("Erro", ane.ParamName, "Fechar");
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