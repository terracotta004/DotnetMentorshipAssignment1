using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

IConfiguration configuration = builder.Build();

// Example: Reading logging configuration
var defaultLogLevel = configuration.GetSection("Logging:LogLevel:Default").Value;
Console.WriteLine($"Default Log Level: {defaultLogLevel}");

// Example: Reading AllowedHosts
var allowedHosts = configuration.GetValue<string>("AllowedHosts");
Console.WriteLine($"Allowed Hosts: {allowedHosts}");

// You can also bind configuration sections to strongly-typed classes
var loggingConfig = configuration.GetSection("Logging").Get<LoggingConfiguration>();

var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

// I set the following environment variable manually in my system settings, 
// but this can also be set programmatically according to the documentation.

bool isEnvironmentVariableGuru = Environment.GetEnvironmentVariable("IS_ENVIRONMENT_VARIABLE_GURU") == "true";
Console.WriteLine($"Is environment variable guru: {isEnvironmentVariableGuru}");

// Here, I am setting an environment variable programmatically. 

Environment.SetEnvironmentVariable("APP_NAME", ".NET Mentorship Assignment 1", EnvironmentVariableTarget.Process);

var appName = Environment.GetEnvironmentVariable("APP_NAME");

if (appName != null && appSettings != null)
{
    appSettings.ApplicationName = appName;
}

Console.WriteLine($"Application Name: {appSettings.ApplicationName}");

Console.WriteLine($"Enable Logging: {appSettings.Features.EnableLogging}");

Console.WriteLine("Modules:");
foreach (var module in appSettings.Features.Modules)
{
    Console.WriteLine($"- {module}");
}

// Define configuration classes
public class LoggingConfiguration
{
    public Dictionary<string, string> LogLevel { get; set; } = new();
}
