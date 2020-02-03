using System.Collections.Generic;
using System.Threading.Tasks;
using CenterEdge.JiraLibrary.Models;

namespace CenterEdge.JiraLibrary.Ports
{
    public interface IJiraRepository
    {
        Task<IList<JiraIssue>> GetJiraKanbanIssuesAsync();
        Task<IList<JiraIssue>> GetJiraKanbanIssuesAsync(string userName);
        Task<JiraIssue> GetJiraIssueAsync(string key);
        Task<IList<JiraIssue>> GetJiraIssuesAsync();
        Task<IList<JiraStatus>> GetJiraStatusesAsync();
        Task<IList<JiraIssueType>> GetJiraIssueTypesAsync();
        Task<IList<JiraResolution>> GetJiraResolutionsAsync();
        Task<IList<JiraProject>> GetJiraProjects();
    }
}
