List<QualityIssue> savedIssues = IssueFileStorage.LoadIssues();

QualityIssueService issueService =
    new QualityIssueService(savedIssues);

Console.WriteLine(
    $"Loaded issues: {issueService.GetAllIssues().Count}");

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
            CreateIssue(issueService);
            break;

        case "2":
            DisplayIssuesMenu(issueService);
            break;

        case "3":
            CloseIssue(issueService);
            break;

        case "4":
            SaveIssues(issueService);
            isRunning = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine(
                "Invalid option. Please select 1, 2, 3, or 4.");
            break;
    }
}

static void CreateIssue(QualityIssueService issueService)
{
    Console.WriteLine("\nCREATE QUALITY ISSUE");
    Console.WriteLine("--------------------");

    string title = ReadRequiredText("Title: ");
    string description = ReadRequiredText("Description: ");

    QualityIssue issue =
        issueService.CreateIssue(title, description);

    bool wasSaved = SaveIssues(issueService);

    Console.WriteLine(
        $"\nIssue #{issue.Id} created successfully.");
    Console.WriteLine($"Status: {issue.Status}");
    Console.WriteLine($"Created: {issue.CreatedAt:g}");

    if (wasSaved)
    {
        Console.WriteLine("Changes saved to issues.json.");
    }
}

static void DisplayIssuesMenu(
    QualityIssueService issueService)
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
            DisplayIssueList(
                issueService.GetAllIssues(),
                "ALL QUALITY ISSUES");
            break;

        case "2":
            DisplayIssueList(
                issueService.GetIssuesByStatus("Open"),
                "OPEN QUALITY ISSUES");
            break;

        case "3":
            DisplayIssueList(
                issueService.GetIssuesByStatus("Closed"),
                "CLOSED QUALITY ISSUES");
            break;

        case "4":
            return;

        default:
            Console.WriteLine("Invalid filter option.");
            break;
    }
}

static void DisplayIssueList(
    IReadOnlyList<QualityIssue> issuesToDisplay,
    string heading)
{
    Console.WriteLine($"\n{heading}");
    Console.WriteLine(new string('-', heading.Length));

    if (issuesToDisplay.Count == 0)
    {
        Console.WriteLine(
            "No matching quality issues were found.");
        return;
    }

    foreach (QualityIssue issue in issuesToDisplay)
    {
        Console.WriteLine($"\nID: {issue.Id}");
        Console.WriteLine($"Title: {issue.Title}");
        Console.WriteLine(
            $"Description: {issue.Description}");
        Console.WriteLine($"Status: {issue.Status}");
        Console.WriteLine($"Created: {issue.CreatedAt:g}");
    }

    Console.WriteLine(
        $"\nTotal matching issues: {issuesToDisplay.Count}");
}

static void CloseIssue(QualityIssueService issueService)
{
    Console.WriteLine("\nCLOSE QUALITY ISSUE");
    Console.WriteLine("-------------------");

    if (issueService.GetAllIssues().Count == 0)
    {
        Console.WriteLine(
            "No quality issues are available to close.");
        return;
    }

    int issueId = ReadIssueId();

    CloseIssueResult result =
        issueService.CloseIssue(issueId);

    switch (result)
    {
        case CloseIssueResult.Success:
            SaveIssues(issueService);
            Console.WriteLine(
                $"Issue #{issueId} was closed successfully.");
            break;

        case CloseIssueResult.NotFound:
            Console.WriteLine(
                $"Issue #{issueId} was not found.");
            break;

        case CloseIssueResult.AlreadyClosed:
            Console.WriteLine(
                $"Issue #{issueId} is already closed.");
            break;
    }
}

static bool SaveIssues(
    QualityIssueService issueService)
{
    List<QualityIssue> issues =
        issueService.GetAllIssues().ToList();

    return IssueFileStorage.SaveIssues(issues);
}

static int ReadIssueId()
{
    while (true)
    {
        Console.Write("Enter the issue ID: ");
        string? idText = Console.ReadLine();

        bool isNumber =
            int.TryParse(idText, out int issueId);

        if (isNumber && issueId > 0)
        {
            return issueId;
        }

        Console.WriteLine(
            "The ID must be a positive whole number.");
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

        Console.WriteLine(
            "This field cannot be empty. Please try again.");
    }
}
