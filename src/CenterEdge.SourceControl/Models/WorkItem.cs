using System;

namespace CenterEdge.SourceControl.Models
{
    public class WorkItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Motivation { get; set; }
        public string Modifications { get; set; }
        public string Actions { get; set; }
        public string Results { get; set; }
        public string Message { get; set; }
        public Version Release { get; set; }
        public string JiraUrl { get; set; }
        public string JiraTag { get; set; }

        public override string ToString()
        {
            return $"[{Title}] Motivation:{Motivation}";
        }
    }
}
