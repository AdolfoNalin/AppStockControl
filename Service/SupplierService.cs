using AppStockControl.ConnectionFactore;
using AppStockControl.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppStockControl.Service
{
    public class SupplierService
    {
        private static readonly string _name = "Supplier";

        #region ChangeStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string> ChangeStatus(Guid id)
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
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
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
        /// Method responsible for create supplier in database 
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string> Create(Supplier supplier)
        {
            HttpClient client = ConnectionLocalhost.ConnectionPostgree();
            HttpResponseMessage response = await client.PostAsJsonAsync($"{_name}", supplier);

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
        #endregion

        #region GetAll
        /// <summary>
        /// Method responsible for get all
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ObservableCollection<Supplier>> GetAll()
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}");

                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<Supplier> suppliers = JsonConvert.DeserializeObject<ObservableCollection<Supplier>>(await response.Content.ReadAsStringAsync());
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
        /// Method responsible for Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Supplier> GetById(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}/ById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Supplier supplier = JsonConvert.DeserializeObject<Supplier>(await response.Content.ReadAsStringAsync());
                    return supplier;
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
        /// Method responsible for bet by Status
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<Supplier>> GetByStatus(bool value)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}/BySatus/{value}");

                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<Supplier> suppliers = JsonConvert.DeserializeObject<ObservableCollection<Supplier>>(await response.Content.ReadAsStringAsync());
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

        #region Update
        /// <summary>
        /// Methdo responsible for update Supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> Update(Supplier supplier)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PutAsJsonAsync($"{_name}", supplier);

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
