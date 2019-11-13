using System.Collections.Generic;
using System.Threading.Tasks;
using CenterEdge.JiraLibrary.Models;

namespace CenterEdge.JiraLibrary.Ports
{
    public interface IJiraRepository
    {
        Task<JiraIssue> GetJiraIssueAsync(string key);
        Task<IList<JiraIssue>> GetJiraIssuesAsync();
    }
}
