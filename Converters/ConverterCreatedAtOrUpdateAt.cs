using AppStockControl.Models;
using System.Globalization;

namespace AppStockControl.Converters
{
    public class ConverterCreatedAtOrUpdateAt : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                if(value is Product product)
                {
                    if(product.UpdatedAt == null)
                    {
                        return product.CreatedAt;
                    }
                    else
                    {
                        return product.UpdatedAt;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
