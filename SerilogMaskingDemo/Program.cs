#region Manually Define Masking 
//var builder = WebApplication.CreateBuilder(args);

//var maskingSettings = builder.Configuration
//    .GetSection("Masking")
//    .Get<MaskingSettings>() ?? new MaskingSettings();

//Enum.TryParse<MaskingStrategy>(maskingSettings.Strategy, ignoreCase: true, out var strategy);
//var masker = new DefaultSensitiveDataMasker(strategy, maskingSettings.Symbol);

//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .Destructure.With(new MaskingDestructuringPolicy(masker))
//    .CreateLogger();

//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog();

//builder.Services.AddSingleton<ISensitiveDataMasker>(masker);

//var app = builder.Build();

//app.MapPost("/pay", (PaymentRequest request) =>
//{
//    Log.Information("Received payment request: {@Request}", request);
//    return Results.Ok("Logged safely!");
//});

//await app.RunAsync();
#endregion

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithSensitiveDataMasking(options =>
    {
        options.MaskingOperators =
        [
            new EmailAddressMaskingOperator(),
            new IbanMaskingOperator(),
            new AmountMaskingOperator()
        ];
    })
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

app.MapPost("/pay", (PaymentRequest request, ILogger<Program> logger) =>
{
    logger.LogInformation("Received payment request from {Email} with IBAN {Iban} and amount {Amount}",
        request.Email,
        request.Iban,
        $"{request.Amount:0.##}");
    return Results.Ok("Logged safely!");
});

await app.RunAsync();


