using System.Text.Json.Serialization;

public class QualityIssue
{
    public int Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public QualityIssue(
        int id,
        string title,
        string description)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = "Open";
        CreatedAt = DateTime.Now;
    }

    [JsonConstructor]
    public QualityIssue(
        int id,
        string title,
        string description,
        string status,
        DateTime createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
    }

    public void UpdateDetails(
        string title,
        string description)
    {
        Title = title;
        Description = description;
    }

    public bool Close()
    {
        if (Status == "Closed")
        {
            return false;
        }

        Status = "Closed";
        return true;
    }
}
