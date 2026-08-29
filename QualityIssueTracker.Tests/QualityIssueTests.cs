using Xunit;

namespace QualityIssueTracker.Tests;

public class QualityIssueTests
{
    [Fact]
    public void Constructor_CreatesIssueWithExpectedValues()
    {
        DateTime beforeCreation = DateTime.Now;

        QualityIssue issue = new QualityIssue(
            1,
            "Bad crimp",
            "Damaged terminal");

        DateTime afterCreation = DateTime.Now;

        Assert.Equal(1, issue.Id);
        Assert.Equal("Bad crimp", issue.Title);
        Assert.Equal("Damaged terminal", issue.Description);
        Assert.Equal("Open", issue.Status);

        Assert.InRange(
            issue.CreatedAt,
            beforeCreation,
            afterCreation);
    }

    [Fact]
    public void Close_WhenIssueIsOpen_ChangesStatusToClosed()
    {
        QualityIssue issue = new QualityIssue(
            1,
            "Bad label",
            "Incorrect serial number");

        bool result = issue.Close();

        Assert.True(result);
        Assert.Equal("Closed", issue.Status);
    }

    [Fact]
    public void Close_WhenIssueIsAlreadyClosed_ReturnsFalse()
    {
        QualityIssue issue = new QualityIssue(
            1,
            "Damaged wire",
            "Insulation is damaged");

        issue.Close();

        bool result = issue.Close();

        Assert.False(result);
        Assert.Equal("Closed", issue.Status);
    }
}
