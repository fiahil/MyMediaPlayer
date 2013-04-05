using System;
using System.Globalization;
using System.Windows.Data;

namespace MyWmp.Converters
{
    class LibraryConverterParam
    {
        public bool Filter { get; set; }
        public bool Group { get; set; }
        public string Value { get; set; }
        public string Sender { get; set; }
    }

    class LibraryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new LibraryConverterParam
                {
                    Filter = (bool) values[0],
                    Group = (bool) values[1],
                    Value = (string) values[2],
                    Sender = (string) values[3]
                };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
