using System;
using System.Text.RegularExpressions;
using MiraNexus.Auth.Models;

namespace MiraNexus.Auth.Services;

public class TokenService
{
    public string GenerateToken()
    {
        string id = Regex.Replace(Guid.NewGuid().ToString(), @"((\w+\-){3}).+", "$1");

        string token = Regex.Replace(id, @"\W", "");

        return token;
    }
}
