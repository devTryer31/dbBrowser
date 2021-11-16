using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using dbBrowser.Data.Model;

namespace dbBrowser.Infrastructure.Converters
{
	public class GenderTypeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null)
				throw new ArgumentNullException(nameof(value));

			if (value is not GenderType gender)
				throw new NotSupportedException("Type not supported");

			return gender switch {
				GenderType.Male => "Муж",
				GenderType.Female => "Жен",
				_ => throw new ArgumentOutOfRangeException(nameof(value)),
			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			if (value is not string strGender)
				throw new NotSupportedException("Type not supported");

			return strGender switch {
				"Муж" => GenderType.Male,
				"Жен" => GenderType.Female,
				_ => throw new ArgumentOutOfRangeException(nameof(value)),
			};
		}
	}
}
