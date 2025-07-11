namespace SerilogMaskingDemo.Infrastructure.Masking;
public sealed class DefaultSensitiveDataMasker(
    MaskingStrategy strategy, 
    char symbol = '*') 
    : ISensitiveDataMasker
{
    public string MaskEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || strategy == MaskingStrategy.None) return email;

        var parts = email.Split('@');
        if (parts.Length != 2) return email;

        var user = parts[0];
        var maskedUser = strategy switch
        {
            MaskingStrategy.Always => new string(symbol, Math.Max(2, user.Length)),
            MaskingStrategy.SensitiveOnly => user[..1] + new string(symbol, Math.Max(1, user.Length - 2)) + user[^1],
            _ => user
        };

        return $"{maskedUser}@{parts[1]}";
    }

    public string MaskIban(string iban)
    {
        if (string.IsNullOrWhiteSpace(iban) || strategy == MaskingStrategy.None) return iban;

        return strategy switch
        {
            MaskingStrategy.Always => new string(symbol, iban.Length),
            MaskingStrategy.SensitiveOnly => iban[..4] + new string(symbol, iban.Length - 8) + iban[^4..],
            _ => iban
        };
    }
}
