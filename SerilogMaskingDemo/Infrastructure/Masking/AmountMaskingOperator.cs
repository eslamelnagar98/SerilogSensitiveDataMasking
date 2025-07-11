namespace SerilogMaskingDemo.Infrastructure.Masking;
public sealed class AmountMaskingOperator : RegexMaskingOperator
{
    private const string _amountRegex = @"(?<!\w)(-?\d{1,3}(?:,\d{3})*(?:\.\d+)?|-?\d+(?:\.\d+)?)(?!\w)";

    public AmountMaskingOperator()
        : base(_amountRegex, RegexOptions.Compiled | RegexOptions.CultureInvariant)
    {
    }

    protected override bool ShouldMaskInput(string input)
    {
        return decimal.TryParse(input, out _);
    }
}
