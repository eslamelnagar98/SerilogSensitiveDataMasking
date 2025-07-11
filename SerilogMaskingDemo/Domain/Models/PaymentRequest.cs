namespace SerilogMaskingDemo.Domain.Models;
public sealed class PaymentRequest
{
    public string Email { get; init; } = string.Empty;
    public string Iban { get; init; } = string.Empty;
    public decimal Amount { get; init; }
}
