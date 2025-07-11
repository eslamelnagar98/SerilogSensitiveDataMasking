# SerilogSensitiveDataMasking

This is a .NET 8 demo project showcasing how to use [Serilog.Enrichers.Sensitive](https://github.com/serilog-contrib/serilog-enrichers-sensitive) to automatically mask sensitive data like **Email addresses**, **IBANs**, and **monetary Amounts** in structured logs.

## ✨ Features

- 🔐 Automatic masking of sensitive values in structured logs
- 🧩 Custom masking operator for amounts (e.g., "Amount": 1000.45 → "***MASKED***")
- 📧 Built-in support for email and IBAN masking
- ✅ Clean architecture using SRP and modern C# practices
- 🧪 Minimal API endpoint for demonstration

## 📦 NuGet Packages

- `Serilog.AspNetCore`
- `Serilog.Enrichers.Sensitive`

## 🚀 How It Works

Sensitive values are masked based on their **property name** or **string content** using `IMaskingOperator`. This allows fine-grained control over what is logged.

### Example Log Output

```log
Received payment request from ***MASKED*** with IBAN ***MASKED*** and amount ***MASKED***
