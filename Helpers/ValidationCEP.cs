using AppStockControl.Models;
using System.Data;

namespace AppStockControl.Helpers
{
    public class ValidationCEP
    {
        public static Address GetCEP(string cep)
        {
			try
			{
				string xmlUrl = "https://viacep.com.br/ws/" + cep + "/xml";
				DataSet ds = new DataSet();
				ds.ReadXml(xmlUrl);
				ds.ReadXml(xmlUrl);

				if (ds.Tables[0].Equals("erro"))
				{
					throw new ArgumentNullException($"CEP {cep} inválido");
				}
				else
				{
					Address address = new Address()
					{
						State = ds.Tables[0].Rows[0]["uf"].ToString(),
						City = ds.Tables[0].Rows[0]["localidade"].ToString(),
						District = ds.Tables[0].Rows[0]["bairro"].ToString(),
						Street = ds.Tables[0].Rows[0]["logradouro"].ToString(),
					};

					return address;
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
    }
}
