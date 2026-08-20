using AppStockControl.DTOs;
using AppStockControl.Models;

namespace AppStockControl.Service
{
    public class AuthService
    {
        public static async Task<bool> AuthUser(Guid id)
        {
			try
			{
				User user = await UserService.GetById(id) ?? throw new ArgumentException();
				LoginRequest loginRequest = new LoginRequest()
				{
					Login = user.Name,
					PasswordHash = user.PasswordHash,
				};

				UserResponse response = await UserService.Login(loginRequest);
				if (response == null)
				{
					return false;
				}
				else
				{
					UserSession.Id = response.Id;
					UserSession.Login = response.Login;
					UserSession.Token = response.Token;

					return true;
				}
			}
			catch(ArgumentException)
			{
				return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }
    }
}
