using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace StarterKit.WPF.Convertors
{
    public class CollectionTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertCollection(value, parameter as Type);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertCollection(value, parameter as Type);
        }

        private static object ConvertCollection(object collection, Type targetItemType)
        {
            if (collection == null || targetItemType == null)
                return null;

            if (collection is IEnumerable sourceCollection)
            {
                try
                {
                    var castMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast))
                    ?.MakeGenericMethod(targetItemType);

                    return castMethod?.Invoke(null, new object[] { sourceCollection });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Ошибка приведения: {ex.Message}");
                    return Enumerable.Empty<object>();
                }
            }

            return null;
        }
    }
}
