using AppStockControl.ConnectionFactore;
using AppStockControl.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppStockControl.Service
{
    public class CategoryService 
    {
        #region ChangeStatus
        /// <summary>
        /// Method responsible for Change status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> ChangeSatus(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.PutAsync($"Category/ChangeStatus/{id}", null);

                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string message = await reponse.Content.ReadAsStringAsync();
                    return message;
                }
                else
                {
                    throw new ArgumentException(await reponse.Content.ReadAsStringAsync());
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

        #region Create
        /// <summary>
        /// Method responsible for Create category in database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static async Task<string> Create(Category category)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.PostAsJsonAsync($"Category/", category);

                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string message = await reponse.Content.ReadAsStringAsync();
                    return message;
                }
                else
                {
                    throw new ArgumentException(await reponse.Content.ReadAsStringAsync());
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

        #region GetAll
        /// <summary>
        /// Method responsible for Get All
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<Category>> GetAll()
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.GetAsync($"Category");

                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<Category> categories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(await reponse.Content.ReadAsStringAsync());
                    return categories;
                }
                else
                {
                    throw new ArgumentException(await reponse.Content.ReadAsStringAsync());
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

        #region GetById
        /// <summary>
        /// Method responsible for Get by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Category> GetById(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.GetAsync($"Category/ById/{id}");

                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Category category = JsonConvert.DeserializeObject<Category>(await reponse.Content.ReadAsStringAsync());
                    return category;
                }
                else
                {
                    throw new ArgumentException(await reponse.Content.ReadAsStringAsync());
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

        #region GetByStatus
        /// <summary>
        /// Method responsible for Get Status
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<ObservableCollection<Category>> GetByStatus(bool value)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.GetAsync($"Category/ByStatus/{value}");

                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<Category> categories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(await reponse.Content.ReadAsStringAsync());
                    return categories;
                }
                else
                {
                    throw new ArgumentException(await reponse.Content.ReadAsStringAsync());
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

        #region Update
        /// <summary>
        /// Method responsible for Update category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static async Task<string> Update(Category category)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.PutAsJsonAsync($"Brand/", category);

                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string message = await reponse.Content.ReadAsStringAsync();
                    return message;
                }
                else
                {
                    throw new ArgumentException(await reponse.Content.ReadAsStringAsync());
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
    }
}
