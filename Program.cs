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
            ShowCreateIssueMessage();
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

static void ShowCreateIssueMessage()
{
    Console.WriteLine("\nCreate issue functionality is coming next.");
}

static void ShowViewIssuesMessage()
{
    Console.WriteLine("\nView issues functionality is coming soon.");
}
