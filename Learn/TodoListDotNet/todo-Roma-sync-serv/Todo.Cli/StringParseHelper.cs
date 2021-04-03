using System;

namespace Todo.Cli
{
    public static class StringParseHelper
    {
        public static int GetRequiredInt(string? input, int defaultValue)
        {
            if (input == null || !int.TryParse(input, out var result))
            {
                return defaultValue;
            }

            return result;
        }

        public static int? GetInt(string? input)
        {
            if (input == null) return null;

            return int.TryParse(input, out var result) ? result : (int?) null;
        }

        public static string GetString(string? input)
        {
            return input ?? string.Empty;
        }

        public static DateTime? GetDate(string? input)
        {
            var (_, date) = TryParseDate(input);

            return date;
        }

        private static (bool success, DateTime? result) TryParseDate(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return (false, default);
            if (DateTime.TryParse(input, out var r)) return (true, r);

            return input.ToLower() switch
            {
                "today"    => (true, DateTime.Today),
                "tomorrow" => (true, DateTime.Today.AddDays(1)),
                _          => (false, default)
            };
        }
    }
}