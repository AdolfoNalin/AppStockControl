using AppStockControl.ConnectionFactore;
using AppStockControl.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppStockControl.Service
{
    public class BrandService
    {
        #region ChangeStatus
        /// <summary>
        /// Method responsible for Change Status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> ChangeSatus(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.PutAsync($"Brand/ChangeStatus/{id}", null);

                if(reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string message = await reponse.Content.ReadAsStringAsync();
                    return message;
                }
                else
                {
                    throw new ArgumentException(await reponse.Content.ReadAsStringAsync());
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
        /// Method responsible for Create Brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public static async Task<string> Create(Brand brand)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.PostAsJsonAsync("Brand", brand);

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
        /// Method responsible for Get all Brands
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<Brand>> GetAll()
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"Brand");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<Brand> brands = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(await response.Content.ReadAsStringAsync());
                    return brands;
                }
                else
                {
                    throw new ArgumentNullException(await response.Content.ReadAsStringAsync());
                }
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetById
        /// <summary>
        /// Method responsible for get brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Brand> GetById(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.GetAsync($"Brand/ById/{id}");

                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Brand brand = JsonConvert.DeserializeObject<Brand>(await reponse.Content.ReadAsStringAsync());
                    return brand;
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
        /// Method responsible for get Brand by status
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<ObservableCollection<Brand>> GetByStatus(bool value)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.GetAsync($"Brand/ByStatus/{value}");

                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<Brand> brands = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(await reponse.Content.ReadAsStringAsync());
                    return brands;
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
        /// Mehtod responsible for Update brand in database
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public static async Task<string> Update(Brand brand)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage reponse = await client.PutAsJsonAsync($"Brand", brand);

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
