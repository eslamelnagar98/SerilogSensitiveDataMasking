namespace SerilogMaskingDemo.Infrastructure.Masking;
public class MaskingDestructuringPolicy(ISensitiveDataMasker masker) : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory factory, out LogEventPropertyValue result)
    {
        if (value is not PaymentRequest request)
        {
            result = null!;
            return false;
        }

        result = new StructureValue(
        [
            new LogEventProperty(nameof(request.Email), new ScalarValue(masker.MaskEmail(request.Email))),
            new LogEventProperty(nameof(request.Iban), new ScalarValue(masker.MaskIban(request.Iban))),
            new LogEventProperty(nameof(request.Amount), new ScalarValue(request.Amount))
        ]);

        return true;
    }
}
