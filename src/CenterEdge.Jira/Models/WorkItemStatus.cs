using System;

namespace CenterEdge.JiraLibrary.Models
{
    [Flags()]
    public enum WorkItemStatus
    {
        Open = 1,
        InProgress = 2,
        PullRequestCreated = 4,
        PullRequestReviewed = 8,
        PullRequestChangesRequested = 16,
        PullRequestApproved = 32,
        ReadyForQA = 64,
        TestingStarted = 128,
        TestingFailed = 256,
        TestingComplete = 512
    }
}
