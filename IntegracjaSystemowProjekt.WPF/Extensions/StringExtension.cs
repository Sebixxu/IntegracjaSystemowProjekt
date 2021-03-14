namespace IntegracjaSystemowProjekt.WPF.Extensions
{
    public static class StringExtension
    {
        public static bool ParseBoolValue(this string value)
        {
            if (bool.TryParse(value, out var result))
                return result;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            if (value == "1" || value.ToLower() == "tak" || value.ToLower() == "yes")
                return true;

            return false;
        }

        public static int? ParseIntValue(this string value)
        {
            if (int.TryParse(value, out var result))
                return result;

            return null;
        }
    }
}