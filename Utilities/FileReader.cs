using System.Text.Json;

public static class FileReader
{
    public static T ReadJsonFile<T>(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json)
                   ?? throw new InvalidOperationException("Deserialization returned null.");
        }
        catch (JsonException ex)
        {
            throw new Exception($"Error parsing JSON from the file at {filePath}: {ex.Message}", ex);
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
        {
            throw new Exception($"Error reading the file at {filePath}: {ex.Message}", ex);
        }

    }
}
