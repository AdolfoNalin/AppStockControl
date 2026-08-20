using AppStockControl.DTOs;
using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;
using CommunityToolkit.Mvvm.Messaging;

namespace AppStockControl.Views;

public partial class SupplierUpdate : ContentPage
{
    private Guid _supplierId;
	public SupplierUpdate()
	{
		InitializeComponent();
	}

	public void SetEdit(Supplier supplier)
	{
        try
        {
            _supplierId = supplier.Id;
            txtName.Text = supplier.Name;
            txtTrandName.Text = supplier.TrandName;
            txtStateRegistration.Text = supplier.StateRegistration;
            txtCnpj.Text = supplier.Cnpj;
            txtEmail.Text = supplier.Email;
            txtCellPhone.Text = supplier.CellPhone;
            txtPhone.Text = supplier.Phone;
            txtConteactName.Text = supplier.ContactName;
            txtZipCode.Text = supplier.Address.ZipCode ?? String.Empty;
            pkUf.ItemDisplayBinding.FallbackValue = supplier.Address.State ?? String.Empty;
        }
        catch(NullReferenceException nre)
        {
            return;
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }

    private async void Button_Clicked_Save(object sender, EventArgs e)
    {
        try
        {
            Supplier supplier = new Supplier()
            {
                Id = _supplierId,
                UserId = UserSession.Id,
                Name = txtName.Text,
                TrandName = txtTrandName.Text,
                StateRegistration = txtStateRegistration.Text,
                Cnpj = txtCnpj.Text,
                Email = txtEmail.Text,
                UpdatedAt = dpDate.Date.Value.Date.ToUniversalTime(),
                CellPhone = txtCellPhone.Text,
                Phone = txtPhone.Text,
                ContactName = txtConteactName.Text,
                Address = new Address()
                {
                    Id = _supplierId,
                    ZipCode = txtZipCode.Text,
                    State = pkUf.Title,
                    City = txtCity.Text,
                    District = txtDistrict.Text,
                    Street = txtStreet.Text,
                    Number = txtNumber.Text,
                    Complement = txtComplement.Text,
                },

                Active = rbEneble.IsChecked ? true : false,
                Observation = txtObservation.Text
            };

            string message = await SupplierService.Update(supplier);

            WeakReferenceMessenger.Default.Send<String>("Supplier");

            DisplayAlert("", message, "Fechar");

            Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
        }
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        Address address = ValidationCEP.GetCEP(txtZipCode.Text);
        
        pkUf.SelectedItem = address.State;
        txtCity.Text = address.City;
        txtDistrict.Text = address.District;
        txtStreet.Text = address.Street;

        txtNumber.Focus();
    }

    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}