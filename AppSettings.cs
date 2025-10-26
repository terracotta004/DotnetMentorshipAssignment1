public class AppSettings
{
    public string ApplicationName { get; set; }
    public Features Features { get; set; }
}

public class Features
{
    public bool EnableLogging { get; set; }
    public string[] Modules { get; set; }
}