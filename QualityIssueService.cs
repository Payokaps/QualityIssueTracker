public enum CloseIssueResult
{
    Success,
    NotFound,
    AlreadyClosed
}

public class QualityIssueService
{
    private readonly List<QualityIssue> _issues;

    public QualityIssueService(
        IEnumerable<QualityIssue>? initialIssues = null)
    {
        _issues = initialIssues?.ToList()
            ?? new List<QualityIssue>();
    }

    public IReadOnlyList<QualityIssue> GetAllIssues()
    {
        return _issues;
    }

    public QualityIssue CreateIssue(
        string title,
        string description)
    {
        int nextId = _issues.Count == 0
            ? 1
            : _issues.Max(issue => issue.Id) + 1;

        QualityIssue issue = new QualityIssue(
            nextId,
            title,
            description);

        _issues.Add(issue);

        return issue;
    }

    public List<QualityIssue> GetIssuesByStatus(string status)
    {
        return _issues
            .Where(issue => issue.Status == status)
            .ToList();
    }

    public QualityIssue? FindIssueById(int issueId)
    {
        return _issues
            .FirstOrDefault(issue => issue.Id == issueId);
    }

    public bool UpdateIssue(
        int issueId,
        string title,
        string description)
    {
        QualityIssue? issue = FindIssueById(issueId);

        if (issue is null)
        {
            return false;
        }

        issue.UpdateDetails(title, description);
        return true;
    }

    public CloseIssueResult CloseIssue(int issueId)
    {
        QualityIssue? issue = FindIssueById(issueId);

        if (issue is null)
        {
            return CloseIssueResult.NotFound;
        }

        bool wasClosed = issue.Close();

        if (!wasClosed)
        {
            return CloseIssueResult.AlreadyClosed;
        }

        return CloseIssueResult.Success;
    }
}
