namespace SimpleSignin.Helpers.Converters;

public class ToggleConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value == null && parameter == null) { return null; }
		string s = (string?)parameter!;
		string[] substrings = s.Split('_');
		if (substrings.Length != 2) { return null; }

		Application.Current!.Resources.TryGetValue(substrings[0], out object on);
		Application.Current!.Resources.TryGetValue(substrings[1], out object off);
		return (bool)value! ? (string)on : (string)off;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return null;
	}
}
