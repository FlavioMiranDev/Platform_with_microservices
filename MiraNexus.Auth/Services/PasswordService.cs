using System;
using System.Text.RegularExpressions;

namespace MiraNexus.Auth.Services;

public class PasswordService
{
    private readonly Regex _regex;

    public PasswordService()
    {
        _regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!*()\-_])[A-Za-z\d@#$%^&+=!*()\-_]{8,}$", RegexOptions.Compiled);
    }

    public bool IsValid(string password)
    {
        return _regex.IsMatch(password);
    }

    public bool IsValidPasswords(string password, string verifyPassword)
    {
        if (string.IsNullOrEmpty(password) && password.Trim() != verifyPassword.Trim()) return false;

        return IsValid(password);
    }
}
