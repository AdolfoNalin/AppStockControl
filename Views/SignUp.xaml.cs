using AppStockControl.Helpers;
using AppStockControl.Models;
using AppStockControl.Service;

namespace AppStockControl.Views;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
	}

    #region Button_Clicked_SaveUser
    private async void Button_Clicked_SaveUser(object sender, EventArgs e)
    {
		try
		{
			if(txtPassword.Text.Equals(txtPasswordConfirmation.Text))
			{
				User user = new User()
				{
					Name = txtLogin.Text,
					Email = txtEmail.Text,
					PasswordHash = txtPassword.Text,
					Active = rbEnable.IsChecked == true ? true : false,
					CreatedAt = DateTime.UtcNow
				};

				string message = await UserService.Create(user);

				DisplayAlert("", message, "Fechar");

				Navigation.PopAsync();
			}
			else
			{
				DisplayAlert("Erro", "Senhas não estão iguais", "Fechar");
			}
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion
}