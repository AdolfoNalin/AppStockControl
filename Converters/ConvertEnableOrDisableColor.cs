using System.Globalization;

namespace AppStockControl.Converters
{
    public class ConvertEnableOrDisableColor : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                bool isActive = (bool)value;

                if (!isActive)
                {
                    return Colors.Red;
                }
                else
                {
                    return Colors.Green;
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

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
