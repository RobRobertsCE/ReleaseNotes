using CenterEdge.JiraLibrary.Models;
using CenterEdge.SourceControl.Models;
using System.Collections.Generic;
using System.Linq;

namespace CenterEdge.SourceControl.TestApp
{
    public class DevWorkItemView
    {
        public WorkItemStatus Status { get; set; }
        public string JiraKey { get; set; }
        public string JiraType { get; set; }
        public string JiraStatus { get; set; }
        public string Developer { get; set; }
        public string AssignedTo { get; set; }
        public string Description { get; set; }
        public string Parent { get; set; }
        public long? PullRequestId { get; set; }
        public int? PullRequestNumber { get; set; }
        public string PullRequestStatus { get; set; }
        public bool? IsReadyForQA { get; set; }
        public bool? IsPatch { get; set; }
        public bool? IsOnHold { get; set; }
        public int SubTaskCount
        {
            get
            {
                return SubTasks.Count;
            }
        }
        public string PullRequestCount
        {
            get
            {
                var subTaskPullRequestCount = SubTasks.Sum(s => s.PullRequests.Count);
                var subTaskPullRequestCountString = subTaskPullRequestCount > 0 ? $" ({subTaskPullRequestCount})" : "";
                return $"{PullRequests.Count} {subTaskPullRequestCountString}".Trim();
            }
        }

        public IList<GitHubPullRequest> PullRequests { get; set; } = new List<GitHubPullRequest>();
        public IList<DevWorkItemView> SubTasks { get; set; } = new List<DevWorkItemView>();
    }
}
