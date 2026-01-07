using System.Text.RegularExpressions;

namespace MiraNexus.Auth.Utils;

public static class DocumentValidation
{
    public static bool ValidarCpfCnpj(string cpfCnpj)
    {
        if (string.IsNullOrWhiteSpace(cpfCnpj))
            return false;

        string numeros = Regex.Replace(cpfCnpj, @"\D", "");

        if (numeros.Length == 11)
            return ValidarCpf(numeros);
        else if (numeros.Length == 14)
            return ValidarCnpj(numeros);

        return false;
    }

    private static bool ValidarCpf(string cpf)
    {
        if (cpf.All(c => c == cpf[0]))
            return false;

        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * (10 - i);

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (digito1 != int.Parse(cpf[9].ToString()))
            return false;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(cpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return digito2 == int.Parse(cpf[10].ToString());
    }

    private static bool ValidarCnpj(string cnpj)
    {
        if (cnpj.All(c => c == cnpj[0]))
            return false;

        int[] peso1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] peso2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(cnpj[i].ToString()) * peso1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (digito1 != int.Parse(cnpj[12].ToString()))
            return false;

        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(cnpj[i].ToString()) * peso2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return digito2 == int.Parse(cnpj[13].ToString());
    }
}
