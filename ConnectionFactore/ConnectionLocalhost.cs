using AppStockControl.DTOs;

namespace AppStockControl.ConnectionFactore
{
    public class ConnectionLocalhost
    {
        private static string GetEndpont()
        {
            try
            {
                string endpoint = MauiProgram.endPointAPI 
                    ?? throw new NullReferenceException("Endpoint não foi encontrado");
                return endpoint;
            }
            catch(NullReferenceException nre)
            {
                throw nre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ConnectionPostgree
        public static HttpClient ConnectionPostgree()
        {
            try
            {
                string endpoint = GetEndpont();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);

                return client;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ConnectionPostgreeUser
        public static HttpClient ConnectionPostgreeUser()
        {
            try
            {
                string endpoint = GetEndpont();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, 0, 30);

                return client;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
