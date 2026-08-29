List<QualityIssue> issues = new List<QualityIssue>();

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("\nQUALITY ISSUE TRACKER");
    Console.WriteLine("---------------------");
    Console.WriteLine("1. Create issue");
    Console.WriteLine("2. View issues");
    Console.WriteLine("3. Exit");

    Console.Write("\nSelect an option: ");
    string? option = Console.ReadLine();

    switch (option)
    {
        case "1":
            CreateIssue(issues);
            break;

        case "2":
            ShowViewIssuesMessage();
            break;

        case "3":
            isRunning = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
            break;
    }
}

static void CreateIssue(List<QualityIssue> issues)
{
    Console.WriteLine("\nCREATE QUALITY ISSUE");
    Console.WriteLine("--------------------");

    string title = ReadRequiredText("Title: ");
    string description = ReadRequiredText("Description: ");

    int nextId = issues.Count + 1;

    QualityIssue issue = new QualityIssue(
        nextId,
        title,
        description);

    issues.Add(issue);

    Console.WriteLine($"\nIssue #{issue.Id} created successfully.");
    Console.WriteLine($"Status: {issue.Status}");
    Console.WriteLine($"Created: {issue.CreatedAt:g}");
    Console.WriteLine($"Issues currently in memory: {issues.Count}");
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

static void ShowViewIssuesMessage()
{
    Console.WriteLine("\nView issues functionality is coming soon.");
}
