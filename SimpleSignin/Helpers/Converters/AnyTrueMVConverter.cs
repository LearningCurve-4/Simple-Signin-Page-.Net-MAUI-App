namespace SimpleSignin.Helpers.Converters;

public class AnyTrueMVConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		foreach (var value in values)
		{
			if (value == null)
			{
				continue;
			}
			else if ((bool)value)
			{
				return true;
			}
		}
		return false;
	}

	public object[]? ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		if (value is not bool b || targetTypes.Any(t => !t.IsAssignableFrom(typeof(bool))))
		{
			// Return null to indicate conversion back is not possible
			return null;
		}

		if (b)
		{
			return targetTypes.Select(t => (object)true).ToArray();
		}
		else
		{
			// Can't convert back from false because of ambiguity
			return null;
		}
	}
}
