namespace AsyncSamples;

public class Constants
{
    public static readonly IEnumerable<string> sUrlList = new string[]
    {
        "https://learn.microsoft.com",
        "https://learn.microsoft.com/aspnet/core",
        "https://learn.microsoft.com/azure",
        "https://learn.microsoft.com/azure/devops",
        "https://learn.microsoft.com/dotnet",
        "https://learn.microsoft.com/dotnet/desktop/wpf/get-started/create-app-visual-studio",
        "https://learn.microsoft.com/education",
        "https://learn.microsoft.com/shows/net-core-101/what-is-net",
        "https://learn.microsoft.com/enterprise-mobility-security",
        "https://learn.microsoft.com/gaming",
        "https://learn.microsoft.com/graph",
        "https://learn.microsoft.com/microsoft-365",
        "https://learn.microsoft.com/office",
        "https://learn.microsoft.com/powershell",
        "https://learn.microsoft.com/sql",
        "https://learn.microsoft.com/surface",
        "https://dotnetfoundation.org",
        "https://learn.microsoft.com/visualstudio",
        "https://learn.microsoft.com/windows"
    };
    
    //The HttpClient exposes the ability to send HTTP requests and receive HTTP responses.
    public static readonly HttpClient client = new HttpClient
    {
        MaxResponseContentBufferSize = 1_000_000
    };
    
    public static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

}