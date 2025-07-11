namespace SerilogMaskingDemo.Settings;
public sealed class MaskingSettings
{
    public string Strategy { get; set; } = "SensitiveOnly";
    public char Symbol { get; set; } = '*';
}
