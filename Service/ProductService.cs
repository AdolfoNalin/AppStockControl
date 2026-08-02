using AppStockControl.ConnectionFactore;
using AppStockControl.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppStockControl.Service
{
    public class ProductService 
    {
        #region GetAll
        /// <summary>
        /// Method responsible for Get all product
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<Product>> GetAll()
        {
            try
            {
                HttpClient client = ConnectionFactore.ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync("Product");

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<Product> collections = new ObservableCollection<Product>();
                    List<Product> products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
                    products.ForEach(p =>collections.Add(p));
                    return collections;
                }
                else
                {
                    throw new ArgumentNullException(await response.Content.ReadAsStringAsync());
                }
            }
            catch(ArgumentNullException ane)
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
        /// Method responsible get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Product> GetById(Guid id)
        {
            try
            {
                HttpClient client = ConnectionFactore.ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"Product/ById/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Product product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
                    return product;
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

        #region ChangeStatus
        /// <summary>
        /// Method responsible for change status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> ChangeStatus(Guid id)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PutAsync($"Product/ChangeStatus/{id}", null);

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                else
                {
                    throw new ArgumentNullException(await response.Content.ReadAsStringAsync());
                }
            }
            catch(ArgumentNullException ane)
            {
                throw ane;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Method responsible for create product in database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static async Task<string> Create(Product product)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PostAsJsonAsync("Product", product);

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                else
                {
                    throw new ArgumentNullException(await response.Content.ReadAsStringAsync());
                }
            }
            catch(ArgumentNullException ae)
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
        /// Mehtod responsible for get product by status
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async static Task<ObservableCollection<Product>> GetByStatus(bool value)
        {
            try
            {
                HttpClient client = ConnectionFactore.ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.GetAsync($"Product/ByStatus/{value}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ObservableCollection<Product> collections = new ObservableCollection<Product>();
                    List<Product> products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
                    products.ForEach(p => collections.Add(p));
                    return collections;
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

        #region Update
        /// <summary>
        /// Method resposnible for update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async static Task<string> Update(Product product)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PutAsJsonAsync("Product", product);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                else
                {
                    throw new ArgumentNullException(await response.Content.ReadAsStringAsync());
                }
            }
            catch (ArgumentNullException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateStock
        /// <summary>
        /// Method responsible for update stock
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="stockQuantity"></param>
        /// <returns></returns>
        public async static Task<bool> UpdateStock(Guid productId, int stockQuantity)
        {
            try
            {
                HttpClient client = ConnectionLocalhost.ConnectionPostgree();
                HttpResponseMessage response = await client.PutAsync($"Product/UpdateStock?id={productId}&stockQuantity={stockQuantity}", null);

                bool result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch (ArgumentNullException ae)
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
