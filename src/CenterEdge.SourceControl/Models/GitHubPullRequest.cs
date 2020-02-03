namespace CenterEdge.SourceControl.Models
{
    public class GitHubPullRequest
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public string JiraKey { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public bool IsReadyForQA { get; set; }
        public bool IsPatch { get; set; }
        public bool IsOnHold { get; set; }
        public bool HasBeenReviewed { get; set; }
        public bool HasRequestedChanges { get; set; }
        public bool IsApproved { get; set; }
    }
}
