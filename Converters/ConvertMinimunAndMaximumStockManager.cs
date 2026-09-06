using AppStockControl.Models;
using System.Globalization;

namespace AppStockControl.Converters
{
    public class ConvertMinimunAndMaximumStockManager : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                if (value is Product product)
                {
                    int mediumStock = product.MaximumStock / 2;

                    if (product.StockQuantity <= mediumStock && product.StockQuantity >= product.MinimumStock)
                    {
                        return Colors.Yellow;
                    }
                    else if (product.StockQuantity > mediumStock && product.StockQuantity < product.MaximumStock)
                    {
                        return Colors.Green;
                    }
                    else if (product.StockQuantity < product.MinimumStock)
                    {
                        return Colors.Red;
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (ArgumentNullException)
            {
                return null;
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
