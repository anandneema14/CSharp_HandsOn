using System.Diagnostics;
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
    
    #region Parallely Reading/Writing in Multiple Files
    
    public static async Task ProcessMultipleWritesAsync()
    {
        IList<FileStream> sourceStreams = new List<FileStream>();

        try
        {
            string folder = Directory.CreateDirectory("tempfolder").Name;
            IList<Task> writeTaskList = new List<Task>();

            for (int index = 1; index <= 10; ++ index)
            {
                string fileName = $"file-{index:00}.txt";
                string filePath = $"{folder}/{fileName}";

                string text = $"In file {index}{Environment.NewLine}";
                byte[] encodedText = Encoding.UTF8.GetBytes(text);

                var sourceStream =
                    new FileStream(
                        filePath,
                        FileMode.Create, FileAccess.Write, FileShare.None,
                        bufferSize: 4096, useAsync: true);

                Task writeTask = sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
                sourceStreams.Add(sourceStream);

                writeTaskList.Add(writeTask);
            }

            await Task.WhenAll(writeTaskList);
        }
        finally
        {
            foreach (FileStream sourceStream in sourceStreams)
            {
                sourceStream.Close();
            }
        }
    }

    public static async Task<string> ProcessMultipleReadsAsync()
    {
        var sb  = new StringBuilder();
        
        foreach (var file in Directory.EnumerateFiles("tempfolder"))
        {
            var sourceStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
            
            byte[] buffer = new byte[0x1000];
            int numRead;
            while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string text = Encoding.UTF8.GetString(buffer, 0, numRead);
                sb.Append(text);
            }
        }
        return sb.ToString();
    }
    
    #endregion
}