using System.Text;

namespace AsyncSamples;

public class AsyncFileAccess
{
    #region WriteText
    public static async Task ProcessWriteAsync(string filePath)
    {
        string text = $"Hello World!{Environment.NewLine}";
        await WriteTextAsync(filePath, text);
    }

    static async Task WriteTextAsync(string filePath, string text)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        
        using var sourceStream = 
            new FileStream(
                filePath, 
                FileMode.Create, 
                FileAccess.Write, 
                FileShare.None, 
                4096, 
                useAsync: true);
        
        await sourceStream.WriteAsync(bytes, 0, bytes.Length);
    }
    #endregion
    
    #region ReadText
    public static async Task ProcessReadAsync(string filePath)
    {
        try
        {
            if (File.Exists(filePath) != false)
            {
                string text = await ReadTextAsync(filePath);
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine($"file not found: {filePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task<string> ReadTextAsync(string filePath)
    {
        using var sourceStream =
            new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
        
        var sb = new StringBuilder();

        byte[] buffer = new byte[0x1000];
        int numRead;
        while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            string text = Encoding.UTF8.GetString(buffer, 0, numRead);
            sb.Append(text);
        }

        return sb.ToString();
    }
    #endregion
}