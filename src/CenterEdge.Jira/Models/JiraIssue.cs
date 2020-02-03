using System.Collections.Generic;

namespace CenterEdge.JiraLibrary.Models
{
    public class JiraIssue
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Developer { get; set; }
        public string AssignedTo { get; set; }
        public string Description { get; set; }
        public string Parent { get; set; }
        public WorkItemStatus WorkItemStatus { get; set; }
        public bool HasPullRequest { get; set; }

        public IList<JiraStatusHistory> History { get; set; } = new List<JiraStatusHistory>();
        public IList<JiraIssue> SubTasks { get; set; } = new List<JiraIssue>();

        public override string ToString()
        {
            return $" Key:{Key} Type:{Type} Status:{Status} Developer:{Developer} Parent:{Parent} Description:{Description}";
        }
    }
}
