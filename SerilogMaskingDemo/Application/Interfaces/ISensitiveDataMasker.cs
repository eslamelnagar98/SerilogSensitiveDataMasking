namespace SerilogMaskingDemo.Application.Interfaces;
public interface ISensitiveDataMasker
{
    string MaskEmail(string email);
    string MaskIban(string iban);
}
