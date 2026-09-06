using System.Globalization;

namespace AppStockControl.Converters
{
    public class ConvertEnableOrDisableText : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                bool isActive = (bool)value;

                if (!isActive)
                {
                    return "Desativado";
                }
                else
                {
                    return "Ativado";
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
