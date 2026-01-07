using System;
using System.Text.RegularExpressions;

namespace MiraNexus.Auth.Utils;

public static class Sanitize
{
    public static string Clear(string input)
    {
        return Regex.Replace(input, @"['"";\\%_\[\]{}|^~`&<>]", @"\$0");
    }
}
