using AppStockControl.ConnectionFactore;
using AppStockControl.DTOs;
using AppStockControl.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppStockControl.Service
{
    public class UserService
    {
        private static readonly string _name = "User";

        #region ChangeStatus
        /// <summary>
        /// Method responsible Change status user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> ChangeStatus(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PutAsync($"{_name}/ChangeStatus/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch(ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Method responsible for create user in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<string> Create(User user)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PostAsJsonAsync($"{_name}", user);

                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Method responsible for get all users
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>    
        /// <exception cref="Exception"></exception>    
        public static async Task<ObservableCollection<User>> GetAll()
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}");

                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<User> suppliers = JsonConvert.DeserializeObject<ObservableCollection<User>>(await response.Content.ReadAsStringAsync());
                    return suppliers;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch(ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetById
        /// <summary>
        /// Method responsible for get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<User> GetById(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}/ById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    User user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                    return user;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch(ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetByStatus
        /// <summary>
        /// Method responsible for get by Status
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static async Task<ObservableCollection<User>> GetByStatus(bool value)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}/ByStatus/{value}");

                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<User> suppliers = JsonConvert.DeserializeObject<ObservableCollection<User>>(await response.Content.ReadAsStringAsync());
                    return suppliers;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch (ArgumentException ae)
            {
                throw ae;   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Login
        /// <summary>
        /// Method responsible for login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static async Task<UserResponse> Login(LoginRequest login)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgreeUser();
                HttpResponseMessage response = await client.PostAsJsonAsync($"User/Login", login);

                if (response.IsSuccessStatusCode)
                {
                    UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(await response.Content.ReadAsStringAsync());
                    return userResponse;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch(ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// method responsible for Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<string> Update(User user)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PutAsJsonAsync($"{_name}", user);

                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }

            }
            catch(ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
