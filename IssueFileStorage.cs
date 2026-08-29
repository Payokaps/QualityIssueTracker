using System.Text.Json;

public static class IssueFileStorage
{
    private const string FileName = "issues.json";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public static List<QualityIssue> LoadIssues()
    {
        try
        {
            if (!File.Exists(FileName))
            {
                return new List<QualityIssue>();
            }

            string json = File.ReadAllText(FileName);

            List<QualityIssue>? issues =
                JsonSerializer.Deserialize<List<QualityIssue>>(
                    json,
                    JsonOptions);

            return issues ?? new List<QualityIssue>();
        }
        catch (JsonException exception)
        {
            Console.WriteLine(
                $"The issue file contains invalid JSON: {exception.Message}");

            return new List<QualityIssue>();
        }
        catch (IOException exception)
        {
            Console.WriteLine(
                $"The issue file could not be read: {exception.Message}");

            return new List<QualityIssue>();
        }
        catch (UnauthorizedAccessException exception)
        {
            Console.WriteLine(
                $"Access to the issue file was denied: {exception.Message}");

            return new List<QualityIssue>();
        }
    }

    public static bool SaveIssues(List<QualityIssue> issues)
    {
        try
        {
            string json = JsonSerializer.Serialize(
                issues,
                JsonOptions);

            File.WriteAllText(FileName, json);
            return true;
        }
        catch (IOException exception)
        {
            Console.WriteLine(
                $"The issues could not be saved: {exception.Message}");

            return false;
        }
        catch (UnauthorizedAccessException exception)
        {
            Console.WriteLine(
                $"Access to the issue file was denied: {exception.Message}");

            return false;
        }
    }
}
