public class QualityIssue
{
    public int Id { get; }

    public string Title { get; }

    public string Description { get; }

    public string Status { get; private set; }

    public DateTime CreatedAt { get; }

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
