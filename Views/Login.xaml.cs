using AppStockControl.DTOs;
using AppStockControl.Helpers;
using AppStockControl.Service;

namespace AppStockControl.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    #region Button_Clicked_Login
    private async void Button_Clicked_Login(object sender, EventArgs e)
    {
		try
		{
			LoginRequest resquest = new LoginRequest()
			{ 
				Login = txtLogin.Text,
				PasswordHash = txtpassword.Text
			};

			UserResponse userResponse = await UserService.Login(resquest);

			if (userResponse != null)
			{
				UserSession.Id = userResponse.Id;
				UserSession.Login = userResponse.Login;
				UserSession.Token = userResponse.Token;

				await SecureStorage.Default.SetAsync("userId", userResponse.Id.ToString());

				Application.Current.MainPage = new AppShell();
			}
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

    #region CheckedChanged_Click
    private void CheckedChanged_Click(object sender, CheckedChangedEventArgs e)
    {
		try
		{
			txtpassword.IsPassword = !txtpassword.IsPassword; 
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion

    #region Label_Click_ForgotPassword
    private void Label_Click_ForgotPassword(object sender, TappedEventArgs e)
    {
		try
		{
			
		}
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion

    #region Button_Clicked_SignUp
    private async void Button_Clicked_SignUp(object sender, EventArgs e)
    {
		try
		{
			SignUp up = this.Handler.MauiContext.Services.GetService<SignUp>();
			Navigation.PushAsync(up);	
        }
		catch (Exception ex)
		{
			DisplayAlert("Erro", MessageException.Message(ex), "Fechar");
		}
    }
    #endregion
}