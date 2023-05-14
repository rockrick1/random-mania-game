using System.Globalization;

public static class StringExtentions
{
    const string ACCURACY_FORMAT = "{0}%";
    const string COMBO_FORMAT = "{0}x";

    public static string FormatScore (this int input) => $"{input:N0}";

    public static string FormatAccuracy (this float input)
    {
        return string.Format(ACCURACY_FORMAT, (input * 100).ToString("F2", CultureInfo.InvariantCulture));
    }
    
    public static string FormatCombo (this int input)
    {
        return string.Format(COMBO_FORMAT, input.ToString(CultureInfo.InvariantCulture));
    }
}