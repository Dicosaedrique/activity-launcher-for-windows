using System.Globalization;
using ActivityLauncher.Domain.Enums;

namespace ActivityLauncher.Domain.Common.Localization;

public static class CultureNames
{
    public const string French = "fr";
    public const string English = "en";

    public const string DefaultLang = English;

    public const Language DefaultLanguage = Language.English;

    public static readonly CultureInfo FrenchCulture = new CultureInfo(French);
    public static readonly CultureInfo EnglishCulture = new CultureInfo(English);

    public static readonly string[] GetSupportedCultureNames = new[] { English, French };

    public static bool IsFrench(this CultureInfo culture)
    {
        return culture.TwoLetterISOLanguageName == French;
    }

    public static bool IsEnglish(this CultureInfo culture)
    {
        return culture.TwoLetterISOLanguageName == English;
    }

    public static CultureInfo GetCulture(this Language language)
    {
        return language switch
        {
            Language.French => FrenchCulture,
            Language.English => EnglishCulture,
            _ => throw new NotImplementedException($"Language.GetCulture() extension method can't process value \"Language.{language}\""),
        };
    }
}
