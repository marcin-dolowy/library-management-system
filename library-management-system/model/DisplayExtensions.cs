using System.Text.RegularExpressions;

namespace library_management_system.model;

public static class DisplayExtensions
{
    public static string ChangeSpacesToDash(this string str)
    {
        return Regex.Replace(str, @"\s+", " - ");
    }
}