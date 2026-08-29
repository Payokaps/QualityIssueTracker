List<QualityIssue> issues = IssueFileStorage.LoadIssues();

Console.WriteLine($"Loaded issues: {issues.Count}");

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("\nQUALITY ISSUE TRACKER");
    Console.WriteLine("---------------------");
    Console.WriteLine("1. Create issue");
    Console.WriteLine("2. View issues");
    Console.WriteLine("3. Close issue");
    Console.WriteLine("4. Exit");

    Console.Write("\nSelect an option: ");
    string? option = Console.ReadLine();

    switch (option)
    {
        case "1":
            CreateIssue(issues);
            break;

        case "2":
            DisplayIssuesMenu(issues);
            break;

        case "3":
            CloseIssue(issues);
            break;

        case "4":
            IssueFileStorage.SaveIssues(issues);
            isRunning = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please select 1, 2, 3, or 4.");
            break;
    }
}

static void CreateIssue(List<QualityIssue> issues)
{
    Console.WriteLine("\nCREATE QUALITY ISSUE");
    Console.WriteLine("--------------------");

    string title = ReadRequiredText("Title: ");
    string description = ReadRequiredText("Description: ");

    int nextId = issues.Count == 0
        ? 1
        : issues.Max(issue => issue.Id) + 1;

    QualityIssue issue = new QualityIssue(
        nextId,
        title,
        description);

    issues.Add(issue);

    bool wasSaved = IssueFileStorage.SaveIssues(issues);

    Console.WriteLine($"\nIssue #{issue.Id} created successfully.");
    Console.WriteLine($"Status: {issue.Status}");
    Console.WriteLine($"Created: {issue.CreatedAt:g}");

    if (wasSaved)
    {
        Console.WriteLine("Changes saved to issues.json.");
    }
}

static void DisplayIssuesMenu(List<QualityIssue> issues)
{
    Console.WriteLine("\nVIEW QUALITY ISSUES");
    Console.WriteLine("-------------------");
    Console.WriteLine("1. View all issues");
    Console.WriteLine("2. View open issues");
    Console.WriteLine("3. View closed issues");
    Console.WriteLine("4. Return to main menu");

    Console.Write("\nSelect a filter: ");
    string? filterOption = Console.ReadLine();

    switch (filterOption)
    {
        case "1":
            DisplayIssueList(issues, "ALL QUALITY ISSUES");
            break;

        case "2":
            List<QualityIssue> openIssues = issues
                .Where(issue => issue.Status == "Open")
                .ToList();

            DisplayIssueList(openIssues, "OPEN QUALITY ISSUES");
            break;

        case "3":
            List<QualityIssue> closedIssues = issues
                .Where(issue => issue.Status == "Closed")
                .ToList();

            DisplayIssueList(closedIssues, "CLOSED QUALITY ISSUES");
            break;

        case "4":
            return;

        default:
            Console.WriteLine("Invalid filter option.");
            break;
    }
}

static void DisplayIssueList(
    List<QualityIssue> issuesToDisplay,
    string heading)
{
    Console.WriteLine($"\n{heading}");
    Console.WriteLine(new string('-', heading.Length));

    if (issuesToDisplay.Count == 0)
    {
        Console.WriteLine("No matching quality issues were found.");
        return;
    }

    foreach (QualityIssue issue in issuesToDisplay)
    {
        Console.WriteLine($"\nID: {issue.Id}");
        Console.WriteLine($"Title: {issue.Title}");
        Console.WriteLine($"Description: {issue.Description}");
        Console.WriteLine($"Status: {issue.Status}");
        Console.WriteLine($"Created: {issue.CreatedAt:g}");
    }

    Console.WriteLine($"\nTotal matching issues: {issuesToDisplay.Count}");
}

static void CloseIssue(List<QualityIssue> issues)
{
    Console.WriteLine("\nCLOSE QUALITY ISSUE");
    Console.WriteLine("-------------------");

    if (issues.Count == 0)
    {
        Console.WriteLine("No quality issues are available to close.");
        return;
    }

    int issueId = ReadIssueId();

    QualityIssue? matchingIssue =
        issues.FirstOrDefault(issue => issue.Id == issueId);

    if (matchingIssue is null)
    {
        Console.WriteLine($"Issue #{issueId} was not found.");
        return;
    }

    bool wasClosed = matchingIssue.Close();

    if (!wasClosed)
    {
        Console.WriteLine($"Issue #{matchingIssue.Id} is already closed.");
        return;
    }

    bool wasSaved = IssueFileStorage.SaveIssues(issues);

    Console.WriteLine($"Issue #{matchingIssue.Id} was closed successfully.");

    if (wasSaved)
    {
        Console.WriteLine("Changes saved to issues.json.");
    }
}

static int ReadIssueId()
{
    while (true)
    {
        Console.Write("Enter the issue ID: ");
        string? idText = Console.ReadLine();

        bool isNumber = int.TryParse(idText, out int issueId);

        if (isNumber && issueId > 0)
        {
            return issueId;
        }

        Console.WriteLine("The ID must be a positive whole number.");
    }
}

static string ReadRequiredText(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? value = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(value))
        {
            return value.Trim();
        }

        Console.WriteLine("This field cannot be empty. Please try again.");
    }
}
