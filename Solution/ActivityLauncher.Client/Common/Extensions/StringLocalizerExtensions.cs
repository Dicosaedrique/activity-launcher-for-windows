using System.Text;
using Microsoft.Extensions.Localization;

namespace ActivityLauncher.Client.Common.Extensions;

public static class StringLocalizerExtensions
{
    public static string Pluralize<T>(this IStringLocalizer<T> localizer, string key, int count, bool formatCount = true)
    {
        if (formatCount) return localizer.Pluralize(key, count, count);
        return localizer.GetString(GetPluralizedKey(key, count));
    }

    public static string Pluralize<T>(this IStringLocalizer<T> localizer, string key, int count, params object[] arguments)
    {
        return localizer.GetString(GetPluralizedKey(key, count), arguments);
    }

    private static string GetPluralizedKey(string translateKey, int count)
    {
        if (count == 0) return $"{translateKey}_zero";
        if (count == 1) return $"{translateKey}_one";
        return translateKey;
    }
}
