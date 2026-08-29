using Xunit;

namespace QualityIssueTracker.Tests;

public class QualityIssueServiceTests
{
    [Fact]
    public void CreateIssue_WhenListIsEmpty_AssignsIdOne()
    {
        QualityIssueService service =
            new QualityIssueService();

        QualityIssue issue = service.CreateIssue(
            "Bad crimp",
            "Damaged terminal");

        Assert.Equal(1, issue.Id);
        Assert.Single(service.GetAllIssues());
    }

    [Fact]
    public void CreateIssue_UsesNextAvailableId()
    {
        List<QualityIssue> existingIssues = new()
        {
            new QualityIssue(2, "Issue 2", "Description"),
            new QualityIssue(5, "Issue 5", "Description")
        };

        QualityIssueService service =
            new QualityIssueService(existingIssues);

        QualityIssue issue = service.CreateIssue(
            "New issue",
            "New description");

        Assert.Equal(6, issue.Id);
    }

    [Fact]
    public void GetIssuesByStatus_ReturnsOnlyMatchingIssues()
    {
        QualityIssue openIssue =
            new QualityIssue(1, "Open issue", "Description");

        QualityIssue closedIssue =
            new QualityIssue(2, "Closed issue", "Description");

        closedIssue.Close();

        QualityIssueService service =
            new QualityIssueService(
                new[] { openIssue, closedIssue });

        List<QualityIssue> results =
            service.GetIssuesByStatus("Closed");

        QualityIssue result = Assert.Single(results);

        Assert.Equal(2, result.Id);
        Assert.Equal("Closed", result.Status);
    }

    [Fact]
    public void CloseIssue_WhenIssueExists_ReturnsSuccess()
    {
        QualityIssue issue =
            new QualityIssue(1, "Bad label", "Description");

        QualityIssueService service =
            new QualityIssueService(new[] { issue });

        CloseIssueResult result = service.CloseIssue(1);

        Assert.Equal(CloseIssueResult.Success, result);
        Assert.Equal("Closed", issue.Status);
    }

    [Fact]
    public void CloseIssue_WhenIdDoesNotExist_ReturnsNotFound()
    {
        QualityIssueService service =
            new QualityIssueService();

        CloseIssueResult result = service.CloseIssue(99);

        Assert.Equal(CloseIssueResult.NotFound, result);
    }

    [Fact]
    public void CloseIssue_WhenAlreadyClosed_ReturnsAlreadyClosed()
    {
        QualityIssue issue =
            new QualityIssue(1, "Bad thread", "Description");

        issue.Close();

        QualityIssueService service =
            new QualityIssueService(new[] { issue });

        CloseIssueResult result = service.CloseIssue(1);

        Assert.Equal(
            CloseIssueResult.AlreadyClosed,
            result);
    }

    [Fact]
    public void UpdateIssue_WhenIssueExists_UpdatesDetails()
    {
        QualityIssue issue = new QualityIssue(
            1,
            "Original title",
            "Original description");

        QualityIssueService service =
            new QualityIssueService(new[] { issue });

        bool result = service.UpdateIssue(
            1,
            "Updated title",
            "Updated description");

        Assert.True(result);
        Assert.Equal("Updated title", issue.Title);
        Assert.Equal(
            "Updated description",
            issue.Description);
    }

    [Fact]
    public void UpdateIssue_WhenIdDoesNotExist_ReturnsFalse()
    {
        QualityIssueService service =
            new QualityIssueService();

        bool result = service.UpdateIssue(
            99,
            "Updated title",
            "Updated description");

        Assert.False(result);
    }
}
