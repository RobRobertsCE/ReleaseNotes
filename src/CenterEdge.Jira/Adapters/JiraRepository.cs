using Atlassian.Jira;
using CenterEdge.JiraLibrary.Data;
using CenterEdge.JiraLibrary.Models;
using CenterEdge.JiraLibrary.Ports;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEdge.JiraLibrary.Adapters
{
    internal class JiraRepository : IJiraRepository
    {
        #region fields

        private readonly IOptions<JiraOptions> _jiraOptions;
        private readonly int _resultsPerPage = 20;

        private IList<JiraStatus> _jiraStatuses = null;

        // plural names just to piss off Bart =P
        private readonly IList<string> _nonClosedStatuses = new List<string>()
        {
            "\"Open\"",
            "\"In Progress\"",
            "\"QA\""
        };
        private readonly IList<string> _parentWorkItemTypes = new List<string>()
        {
            "\"Bug\"",
            "\"User Story\"",
            "\"Tech Story\"",
        };
        private readonly IList<string> _subTaskWorkItemTypes = new List<string>()
        {
            "\"Sub-task\"",
            "\"Dev Sub-task\"",
            "\"QA Bug Sub-task\"",
            "\"Testing Sub-task\""
        };

        #endregion

        #region ctor

        public JiraRepository(IOptions<JiraOptions> jiraOptions)
        {
            _jiraOptions = jiraOptions ?? throw new ArgumentNullException(nameof(jiraOptions));
        }

        #endregion

        #region public

        public async Task<IList<JiraProject>> GetJiraProjects()
        {
            IList<JiraProject> jiraProjects = new List<JiraProject>();

            var jira = GetJiraClient();

            var projects = await jira.Projects.GetProjectsAsync();

            foreach (var project in projects)
            {
                jiraProjects.Add(ToJiraProject(project));
                System.Console.WriteLine($"Name:{project.Name}, Key:{project.Key}");
            }

            return jiraProjects;
        }

        public async Task<JiraIssue> GetJiraIssueAsync(string key)
        {
            var jira = GetJiraClient();

            var issue = await jira.Issues.GetIssueAsync(key);

            return await ToJiraIssueAsync(issue);
        }

        public async Task<IList<JiraIssue>> GetJiraIssuesAsync()
        {
            var jiraIssues = new List<JiraIssue>();

            var jira = GetJiraClient();

            var issues = await SearchAsync(jira, _jiraOptions.Value.projectKey, _jiraOptions.Value.team);

            foreach (var issue in issues)
            {
                jiraIssues.Add(await ToJiraIssueAsync(issue));
            }

            return jiraIssues;
        }

        public async Task<IList<JiraStatus>> GetJiraStatusesAsync()
        {
            var jiraStatusList = new List<JiraStatus>();

            var jira = GetJiraClient();

            var statuses = await jira.Statuses.GetStatusesAsync();

            foreach (var status in statuses.ToList())
            {
                var jiraStatus = new JiraStatus()
                {
                    Id = status.Id,
                    Name = status.Name,
                    Description = status.Description,
                    IconUrl = status.IconUrl
                };

                jiraStatusList.Add(jiraStatus);
            }

            return jiraStatusList;
        }

        public async Task<IList<JiraIssueType>> GetJiraIssueTypesAsync()
        {
            var jiraItemList = new List<JiraIssueType>();

            var jira = GetJiraClient();

            var jiraItems = await jira.IssueTypes.GetIssueTypesForProjectAsync(_jiraOptions.Value.projectKey);

            foreach (var item in jiraItems.ToList())
            {
                var jiraItem = new JiraIssueType()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    IconUrl = item.IconUrl
                };

                jiraItemList.Add(jiraItem);
            }

            return jiraItemList;
        }

        public async Task<IList<JiraResolution>> GetJiraResolutionsAsync()
        {
            var jiraItemList = new List<JiraResolution>();

            var jira = GetJiraClient();

            var jiraItems = await jira.Resolutions.GetResolutionsAsync();

            foreach (var item in jiraItems.ToList())
            {
                var jiraItem = new JiraResolution()
                {
                    Id = item.Id,
                    Name = item.Name
                };

                jiraItemList.Add(jiraItem);
            }

            return jiraItemList;
        }

        public async Task<IList<JiraIssue>> GetJiraKanbanIssuesAsync()
        {
            return await GetJiraKanbanIssuesAsync(string.Empty);
        }

        public async Task<IList<JiraIssue>> GetJiraKanbanIssuesAsync(string userName)
        {
            var jiraIssues = new List<JiraIssue>();

            var jira = GetJiraClient();



            return await SearchKanbanAsync(jira, userName);
        }

        #endregion

        #region protected

        protected virtual async Task<IList<JiraIssue>> SearchKanbanAsync(Jira jira, string userName = null)
        {
            IList<JiraIssue> issues = new List<JiraIssue>();

            int pageNumber = 0;

            var jql = GetKanbanTasksJqlFilter(userName);

            IPagedQueryResult<Issue> pagedIssues;

            do
            {
                pagedIssues = await GetPagedResultsAsync(jira, jql, pageNumber, _resultsPerPage);

                foreach (var issue in pagedIssues)
                {
                    var jiraIssue = await ToJiraIssueAsync(issue);

                    var subTasks = await jira.Issues.GetSubTasksAsync(issue.Key.Value, 100, 0);

                    foreach (Issue subTask in subTasks)
                    {
                        jiraIssue.SubTasks.Add(await ToJiraIssueAsync(subTask));
                    }

                    issues.Add(jiraIssue);
                }

                pageNumber += 1;

            } while (pagedIssues.ItemsPerPage * pageNumber < pagedIssues.TotalItems);

            return issues;
        }

        protected virtual async Task<IList<Issue>> SearchAsync(Jira jira, string projectKey, string team)
        {
            IList<Issue> issues = new List<Issue>();
            IPagedQueryResult<Issue> pagedIssues;
            int pageNumber = 0;

            var jql = $"{GetProjectJqlFilter(projectKey)} " +
                     $"AND {GetTeamJqlFilter(team)} ";

            do
            {
                pagedIssues = await GetPagedResultsAsync(jira, jql, pageNumber, _resultsPerPage);

                foreach (var issue in pagedIssues)
                {
                    issues.Add(issue);
                }

                pageNumber += 1;

            } while (pagedIssues.ItemsPerPage * pageNumber < pagedIssues.TotalItems);

            return issues;
        }

        protected virtual async Task<IPagedQueryResult<Issue>> GetPagedResultsAsync(Jira jira, string jql, int pageNumber, int resultsPerPage)
        {
            IList<Issue> issues = new List<Issue>();

            IssueSearchOptions options = new IssueSearchOptions(jql)
            {
                MaxIssuesPerRequest = resultsPerPage,
                StartAt = resultsPerPage * pageNumber,
                FetchBasicFields = true
            };

            return await jira.Issues.GetIssuesFromJqlAsync(options);
        }

        protected virtual async Task<JiraIssue> ToJiraIssueAsync(Issue issue)
        {
            var jiraIssue = new JiraIssue()
            {
                Key = issue.Key.Value,
                Type = issue?.Type.Name,
                Status = issue.Status.Name,
                Description = issue.Summary,
                Parent = issue.ParentIssueKey,
                Developer = issue.CustomFields["Developer"]?.Values[0].ToString(),
                HasPullRequest = !String.IsNullOrEmpty(issue.CustomFields["Development"]?.Values[0]),
                AssignedTo = issue.Assignee,
                WorkItemStatus = WorkItemStatus.Open
            };

            jiraIssue.History = await GetWorkItemHistoryAsync(issue);

            if (!String.IsNullOrEmpty(issue.CustomFields["Sent to In Progress"]?.Values[0]))
            {
                jiraIssue.WorkItemStatus |= WorkItemStatus.InProgress;
            }
            if (!String.IsNullOrEmpty(issue.CustomFields["Sent to Code Review"]?.Values[0]))
            {
                jiraIssue.WorkItemStatus |= WorkItemStatus.PullRequestCreated;
            }
            if (!String.IsNullOrEmpty(issue.CustomFields["Sent to QA"]?.Values[0]))
            {
                jiraIssue.WorkItemStatus |= WorkItemStatus.TestingStarted;
            }
            if (issue.ResolutionDate != null)
            {
                jiraIssue.WorkItemStatus |= WorkItemStatus.TestingComplete;
            }
            if (issue.Status.Name == "In Progress" && jiraIssue.History.Any(h => h.Status.Name == "Code Review"))
            {
                // changes requested
                jiraIssue.WorkItemStatus |= WorkItemStatus.PullRequestChangesRequested;
            }
            if (issue.Status.Name == "In Progress" && jiraIssue.History.Any(h => h.Status.Name == "QA"))
            {
                // returned from QA
                jiraIssue.WorkItemStatus |= WorkItemStatus.TestingFailed;
            }

            return jiraIssue;
        }

        protected virtual JiraProject ToJiraProject(Project project)
        {
            return new JiraProject()
            {
                Key = project.Key,
                Name = project.Name
            };
        }

        #endregion

        #region private

        private async Task<IList<JiraStatus>> GetStatusListAsync()
        {
            if (_jiraStatuses == null)
            {
                _jiraStatuses = await GetJiraStatusesAsync();
            }

            return _jiraStatuses;
        }

        private async Task<IList<JiraStatusHistory>> GetWorkItemHistoryAsync(Issue issue)
        {
            IList<JiraStatusHistory> history = new List<JiraStatusHistory>();
            IList<JiraStatus> statuses = await GetStatusListAsync();

            //           [CHART] Time in Status
            //1_*:*_1_*:*_258834102_*|
            //*_3_*:*_2_*:*_9461399_*|
            //*_10100_*:*_2_*:*_229723943_*|
            //*_11301_*:*_2_*:*_27464933
            if (!String.IsNullOrEmpty(issue.CustomFields["[CHART] Time in Status"]?.Values[0]))
            {
                var statusHistoryString = issue.CustomFields["[CHART] Time in Status"].Values[0];

                var statusHistoryEntries = statusHistoryString.Split('|');

                for (int i = 0; i < statusHistoryEntries.Length; i++)
                {
                    var statusHistoryEntry = statusHistoryEntries[i];

                    var statusHistoryEntrySections = statusHistoryEntry.Split(':');

                    var statusId = statusHistoryEntrySections[0].Replace("*_", "").Replace("_*", "").Trim();
                    var count = statusHistoryEntrySections[1].Replace("*_", "").Replace("_*", "").Trim();
                    var duration = statusHistoryEntrySections[2].Replace("*_", "").Replace("_*", "").Trim();
                    var status = statuses.FirstOrDefault(s => s.Id == statusId);

                    var statusHistory = new JiraStatusHistory()
                    {
                        Index = i,
                        Count = int.Parse(count),
                        Duration = long.Parse(duration),
                        Status = status
                    };

                    history.Add(statusHistory);
                }
            }

            return history;
        }

        private Jira GetJiraClient()
        {
            return Jira.CreateRestClient(
                $"https://{_jiraOptions.Value.companyName}.atlassian.net",
                _jiraOptions.Value.userEMail,
                _jiraOptions.Value.apiKey);
        }

        private string GetProjectJqlFilter()
        {
            return GetProjectJqlFilter(_jiraOptions.Value.projectKey);
        }
        private string GetProjectJqlFilter(string projectKey)
        {
            return $"project = {projectKey}";
        }
        private string GetProjectJqlFilter(IEnumerable<string> projectKeys)
        {
            return $"project in ({string.Join(", ", projectKeys)}) ";
        }

        private string GetResolvedLast21DaysJqlFilter()
        {
            return "resolved >= -21d ";
        }
        private string GetUpdatedLastNDaysJqlFilter(int daysBack)
        {
            return $"updated >= \"-{daysBack}d\" ";
        }

        private string GetTeamJqlFilter()
        {
            return GetTeamJqlFilter(_jiraOptions.Value.team);
        }
        private string GetTeamJqlFilter(string team)
        {
            return $"\"CE Team\" = \"{team}\" ";
        }

        private string GetNonClosedJqlFilter()
        {
            return $"status in ({string.Join(", ", _nonClosedStatuses)}) ";
        }

        private string GetParentWorkItemTypesJqlFilter()
        {
            return $"type in ({string.Join(", ", _parentWorkItemTypes)}) ";
        }
        private string GetSubTaskWorkItemTypesJqlFilter()
        {
            return $"type in ({string.Join(", ", _subTaskWorkItemTypes)}) ";
        }

        private string GetKanbanTasksJqlFilter(string userName = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{GetProjectJqlFilter()} AND ");
            sb.Append($"{GetTeamJqlFilter()} AND ");
            sb.Append($"{GetNonClosedJqlFilter()} AND ");
            sb.Append($"{GetParentWorkItemTypesJqlFilter()} AND ");
            sb.Append($"{GetUpdatedLastNDaysJqlFilter(180)} ");

            if (!string.IsNullOrWhiteSpace(userName))
            {
                sb.Append($"AND (assignee=\"{userName}\" OR developer=\"{userName}\")");
            }

            return sb.ToString();
        }
        private string GetKanbanSubTaskJqlFilter(string parentKey)
        {
            return $"{GetProjectJqlFilter()} AND {GetTeamJqlFilter()} AND {GetNonClosedJqlFilter()} AND {GetSubTaskWorkItemTypesJqlFilter()} ";// AND ParentIssueKey = {parentKey}";
        }

        #endregion
    }
}
