namespace CenterEdge.JiraLibrary.Models
{
    public class JiraStatusHistory
    {
        public int Index { get; set; }
        public int Count { get; set; }
        public JiraStatus Status { get; set; }
        public long Duration { get; set; }
    }
}
