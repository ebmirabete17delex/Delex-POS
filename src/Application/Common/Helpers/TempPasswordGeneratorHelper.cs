using System.Security.Cryptography;
namespace Delex_POS.Application.Common.Helpers;

public static class TemporaryPasswordGeneratorHelper
{
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string Special = "!@#$%^&*()_-+=[{]};:>|./?";

    public static string Generate(int length = 12)
    {
        if (length < 8)
            throw new ArgumentException("Password length must be at least 8 characters.", nameof(length));

        // 1. Ensure at least one character from each required set is selected
        var passwordChars = new char[length];
        passwordChars[0] = GetRandomChar(Lowercase);
        passwordChars[1] = GetRandomChar(Uppercase);
        passwordChars[2] = GetRandomChar(Digits);
        passwordChars[3] = GetRandomChar(Special);

        // 2. Fill the remaining characters from the combined pool
        string allChars = Lowercase + Uppercase + Digits + Special;
        for (int i = 4; i < length; i++)
        {
            passwordChars[i] = GetRandomChar(allChars);
        }

        // 3. Shuffle the array using a cryptographically secure method to hide patterns
        Shuffle(passwordChars);

        return new string(passwordChars);
    }

    private static char GetRandomChar(string charPool)
    {
        // Use RandomNumberGenerator to securely get a random index
        int index = RandomNumberGenerator.GetInt32(charPool.Length);
        return charPool[index];
    }

    private static void Shuffle(char[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = RandomNumberGenerator.GetInt32(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }
}
