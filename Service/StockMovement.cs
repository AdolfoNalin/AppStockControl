using AppStockControl.ConnectionFactore;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppStockControl.Service
{
    internal class StockMovement 
    {
        private static readonly string _name = "StockMovement";

        #region Create
        /// <summary>
        /// Method responsible for Create StockMovement in database
        /// </summary>
        /// <param name="stockMovement"></param>
        /// <returns></returns>
        public static async Task<bool> Create(StockMovement stockMovement)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PostAsJsonAsync($"{_name}", stockMovement);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    bool success = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                    return success;
                }
                else
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
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
        /// Method responsible for Get All StockMovement
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<StockMovement>> GetAll()
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<StockMovement> stocks = JsonConvert.DeserializeObject<ObservableCollection<StockMovement>>(await response.Content.ReadAsStringAsync());
                    return stocks;
                }
                else
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
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

        #region GetByDate
        /// <summary>
        /// Method responsible for Get by data
        /// </summary>
        /// <param name="startDate">initial date</param>
        /// <param name="endDate">End Date</param>
        /// <returns></returns>
        public static async Task<ObservableCollection<StockMovement>> GetByDate(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}/ByDate?startDate={startDate}&endDate={endDate}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<StockMovement> stocks = JsonConvert.DeserializeObject<ObservableCollection<StockMovement>>(await response.Content.ReadAsStringAsync());
                    return stocks;
                }
                else
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
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
        /// Method responsible for get StockMovement in database for id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<StockMovement> GetById(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}/ById/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    StockMovement stock = JsonConvert.DeserializeObject<StockMovement>(await response.Content.ReadAsStringAsync());
                    return stock;
                }
                else
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
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

        #region GetByProductId
        /// <summary>
        /// Method responsible for Get by productId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<ObservableCollection<StockMovement>> GetByProductId(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"{_name}/ByProductId/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<StockMovement> stocks = JsonConvert.DeserializeObject<ObservableCollection<StockMovement>>(await response.Content.ReadAsStringAsync());
                    return stocks;
                }
                else
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
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

        #region GetByMovementType
        /// <summary>
        /// Method responsible for get by movement type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<ObservableCollection<StockMovement>> GetByMovementType(string value)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PutAsync($"{_name}/ByMovementType/{value}", null);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<StockMovement> list = JsonConvert.DeserializeObject<ObservableCollection<StockMovement>>(await response.Content.ReadAsStringAsync());
                    return list;
                }
                else
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
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
        /// Method responsible for update StockMoviment
        /// </summary>
        /// <param name="stockMovement"></param>
        /// <returns></returns>
        public static async Task<bool> Update(StockMovement stockMovement)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PutAsJsonAsync($"{_name}", stockMovement);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    bool success = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                    return success;
                }
                else
                {
                    throw new ArgumentException(await response.Content.ReadAsStringAsync());
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
