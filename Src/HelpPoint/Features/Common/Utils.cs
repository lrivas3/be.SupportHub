namespace HelpPoint.Features.Common;

public static class Utils
{
    public static string CreateAvatarLetters(string name, string lastname) =>
        string.Concat(name.AsSpan(0, 1), lastname.AsSpan(0, 1))
            .ToUpper(System.Globalization.CultureInfo.CurrentCulture);
}
