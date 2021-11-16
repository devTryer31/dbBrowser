using System;
using System.Globalization;
using System.Windows.Data;
using dbBrowser.Data.Model;

namespace dbBrowser.Infrastructure.Converters
{
	public class ParentTypeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null)
				throw new ArgumentNullException(nameof(value));

			if (value is not FamilyRelationType parentType)
				throw new NotSupportedException("Type not supported");

			return parentType switch {
				FamilyRelationType.Mother => "Мать",
				FamilyRelationType.Father => "Отец",
				_ => throw new ArgumentOutOfRangeException(nameof(value)),
			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null)
				throw new ArgumentNullException(nameof(value));
			if (value is not string strValue)
				throw new NotSupportedException("Type not supported");

			return strValue switch {
				"Мать" => FamilyRelationType.Mother,
				"Отец" => FamilyRelationType.Father,
				_ => throw new ArgumentOutOfRangeException(nameof(value))
			};
		}
	}
}
