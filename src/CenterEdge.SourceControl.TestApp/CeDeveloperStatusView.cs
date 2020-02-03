using CenterEdge.JiraLibrary;
using CenterEdge.JiraLibrary.Models;
using CenterEdge.JiraLibrary.Ports;
using CenterEdge.SourceControl.GitHub;
using CenterEdge.SourceControl.Models;
using CenterEdge.SourceControl.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CenterEdge.SourceControl.TestApp
{
    public partial class CeDeveloperStatusView : Form
    {
        #region fields

        IServiceProvider _provider;
        IList<GitHubPullRequest> _pullRequests;
        IList<JiraIssue> _jiraIssues;
        IList<DevWorkItemView> _workItems;

        #endregion

        #region ctor

        public CeDeveloperStatusView()
        {
            InitializeComponent();
        }

        #endregion

        #region private (event handlers)

        private void CeDeveloperStatusView_Load(object sender, EventArgs e)
        {
            try
            {
                _provider = GetServiceProvider();

                InitializeUserList();

                lblAppStatus.Text = "Ready";
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            await ReloadKanbanDataAsync();
        }

        private void chkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            timAutoUpdate.Enabled = chkAutoUpdate.Checked;
        }

        private async void timAutoUpdate_Tick(object sender, EventArgs e)
        {
            await ReloadKanbanDataAsync();
        }

        private void dgvWorkItems_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedWorkItem();
        }

        private void dgvSubTasks_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedSubTask();
        }

        private void dgvWorkItems_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateSelectedWorkItem();
        }

        private void dgvWorkItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvWorkItems.Columns["Parent"].Visible = false;
            dgvWorkItems.Columns["PullRequests"].Visible = false;
            dgvWorkItems.Columns["SubTasks"].Visible = false;
            dgvWorkItems.Columns["PullRequestId"].HeaderText = "PR Id";
            dgvWorkItems.Columns["PullRequestNumber"].HeaderText = "PR #";
            dgvWorkItems.Columns["PullRequestStatus"].HeaderText = "PR Status";

            dgvWorkItems.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            foreach (DataGridViewRow row in dgvWorkItems.Rows)
            {
                WorkItemStatus cellStatus = (WorkItemStatus)row.Cells["Status"].Value;

                if (cellStatus.HasFlag(WorkItemStatus.InProgress))
                {
                    row.Cells["Status"].Style.BackColor = Color.LightSteelBlue;
                }
                if (cellStatus.HasFlag(WorkItemStatus.PullRequestChangesRequested))
                {
                    row.Cells["Status"].Style.BackColor = Color.Orange;
                }
                if (cellStatus.HasFlag(WorkItemStatus.TestingFailed))
                {
                    row.Cells["Status"].Style.BackColor = Color.Red;
                }
                if (cellStatus.HasFlag(WorkItemStatus.TestingComplete))
                {
                    row.Cells["Status"].Style.BackColor = Color.LimeGreen;
                }
                row.Cells["Status"].Style.SelectionBackColor = row.Cells["Status"].Style.BackColor;
                row.Cells["Status"].Style.SelectionForeColor = row.Cells["Status"].Style.ForeColor;
            }
        }

        private void dgvSubTasks_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvSubTasks.Columns["PullRequests"].Visible = false;
            dgvSubTasks.Columns["SubTasks"].Visible = false;
            dgvSubTasks.Columns["PullRequestId"].HeaderText = "PR Id";
            dgvSubTasks.Columns["PullRequestNumber"].HeaderText = "PR #";
            dgvSubTasks.Columns["PullRequestStatus"].HeaderText = "PR Status";

            dgvSubTasks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            foreach (DataGridViewRow row in dgvSubTasks.Rows)
            {
                WorkItemStatus cellStatus = (WorkItemStatus)row.Cells["Status"].Value;

                if (cellStatus.HasFlag(WorkItemStatus.InProgress))
                {
                    row.Cells["Status"].Style.BackColor = Color.LightSteelBlue;
                }
                if (cellStatus.HasFlag(WorkItemStatus.PullRequestChangesRequested))
                {
                    row.Cells["Status"].Style.BackColor = Color.Orange;
                }
                if (cellStatus.HasFlag(WorkItemStatus.TestingFailed))
                {
                    row.Cells["Status"].Style.BackColor = Color.Red;
                }
                if (cellStatus.HasFlag(WorkItemStatus.TestingComplete))
                {
                    row.Cells["Status"].Style.BackColor = Color.LimeGreen;
                }
                row.Cells["Status"].Style.SelectionBackColor = row.Cells["Status"].Style.BackColor;
                row.Cells["Status"].Style.SelectionForeColor = row.Cells["Status"].Style.ForeColor;
            }
        }

        private void dgvPullRequests_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPullRequests.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            foreach (DataGridViewRow row in dgvPullRequests.Rows)
            {
                GitHubPullRequest pullRequest = row.DataBoundItem as GitHubPullRequest;

                if (pullRequest.IsApproved)
                {
                    row.Cells["Status"].Style.BackColor = Color.LimeGreen;
                }
                if (pullRequest.HasRequestedChanges)
                {
                    row.Cells["Status"].Style.BackColor = Color.Red;
                }
                if (pullRequest.IsOnHold)
                {
                    row.Cells["Status"].Style.BackColor = Color.Gold;
                }
                row.Cells["Status"].Style.SelectionBackColor = row.Cells["Status"].Style.BackColor;
                row.Cells["Status"].Style.SelectionForeColor = row.Cells["Status"].Style.ForeColor;
            }
        }

        #endregion

        #region private

        private void ExceptionHandler(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            MessageBox.Show(ex.Message);
        }

        private void SetFormBusy()
        {
            lblAppStatus.Text = "Working...";

            pnlTop.Enabled = false;
            pnlWorkItems.Enabled = false;
            pnlSubTasks.Enabled = false;
            pnlPullRequests.Enabled = false;
        }

        private void SetFormReady()
        {
            lblAppStatus.Text = "Ready";

            pnlTop.Enabled = true;
            pnlWorkItems.Enabled = true;
            pnlSubTasks.Enabled = true;
            pnlPullRequests.Enabled = true;
        }

        private IServiceProvider GetServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            services.AddGitHub(configuration);
            services.AddJira(configuration);

            var serviceProvider = services.BuildServiceProvider(true);

            return serviceProvider;
        }

        private void InitializeUserList()
        {
            var users = new List<string>()
            {
                "-All-",
                "bsferrazza",
                "kfriedl",
                "lpowell",
                "prutledge",
                "rroberts",
                "tlivengood"
            };

            cboUserFilter.DataSource = users;
            cboUserFilter.SelectedIndex = 0;
        }

        private async Task ReloadKanbanDataAsync()
        {
            try
            {
                SetFormBusy();

                ClearPullRequests();

                ClearSubTasks();

                ClearWorkItems();

                await LoadJiraKanbanIssuesAsync();

                await LoadGitHubItemsAsync();

                _workItems = BuildDevWorkItemViews();

                DisplayDevWorkItemViews(_workItems);
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
            finally
            {
                SetFormReady();
            }
        }

        private void UpdateSelectedWorkItem()
        {
            try
            {
                ClearSubTasks();

                ClearPullRequests();

                if (dgvWorkItems.SelectedRows.Count > 0)
                {
                    DevWorkItemView workItem = dgvWorkItems.SelectedRows[0].DataBoundItem as DevWorkItemView;

                    DisplaySubTasks(workItem?.SubTasks);

                    DisplayPullRequests(workItem?.PullRequests);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        private void UpdateSelectedSubTask()
        {
            try
            {
                ClearPullRequests();

                if (dgvSubTasks.SelectedRows.Count > 0)
                {
                    DevWorkItemView workItem = dgvSubTasks.SelectedRows[0].DataBoundItem as DevWorkItemView;

                    DisplayPullRequests(workItem?.PullRequests);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        private async Task LoadJiraKanbanIssuesAsync()
        {
            var jiraRepository = _provider.GetRequiredService<IJiraRepository>();

            string userFilter = null;

            if (cboUserFilter.SelectedIndex > 0)
            {
                userFilter = cboUserFilter.SelectedItem.ToString();
            }

            var issues = await jiraRepository.GetJiraKanbanIssuesAsync(userFilter);

            _jiraIssues = issues;//.Where(i => (i.Status == "Open" || i.Status == "QA") && (i.AssignedTo == "rroberts" || i.Developer == "rroberts")).OrderBy(i => i.Key).ToList();
        }

        private async Task LoadGitHubItemsAsync()
        {
            IWorkItemRepository workItemRepository = _provider.GetRequiredService<IWorkItemRepository>();

            _pullRequests = await workItemRepository.GetPullRequestsAsync();
        }

        private IList<DevWorkItemView> BuildDevWorkItemViews()
        {
            IList<DevWorkItemView> workItems = new List<DevWorkItemView>();

            foreach (var jiraIssue in _jiraIssues)
            {
                IEnumerable<GitHubPullRequest> pullRequests = _pullRequests.Where(p => p.JiraKey == jiraIssue.Key);

                var workItem = GetDevWorkItemView(jiraIssue, pullRequests);

                workItems.Add(workItem);
            }

            return workItems;
        }

        private DevWorkItemView GetDevWorkItemView(JiraIssue jiraIssue, IEnumerable<GitHubPullRequest> pullRequests)
        {
            var workItem = new DevWorkItemView()
            {
                JiraKey = jiraIssue.Key,
                JiraStatus = jiraIssue.Status,
                AssignedTo = jiraIssue.AssignedTo,
                Description = jiraIssue.Description,
                Developer = jiraIssue.Developer,
                Parent = jiraIssue.Parent,
                JiraType = jiraIssue.Type,

            };

            workItem.Status = GetWorkItemStatus(jiraIssue, pullRequests);

            workItem.PullRequests = pullRequests.ToList();

            if (pullRequests.Count() == 1)
            {
                var pullRequest = pullRequests.First();

                workItem.PullRequestId = pullRequest.Id;
                workItem.PullRequestNumber = pullRequest.Number;
                workItem.PullRequestStatus = pullRequest.Status;
                workItem.IsReadyForQA = pullRequest.IsReadyForQA;
                workItem.IsPatch = pullRequest.IsPatch;
                workItem.IsOnHold = pullRequest.IsOnHold;
            }

            foreach (JiraIssue subTask in jiraIssue.SubTasks)
            {
                var subTaskPullRequest = _pullRequests.Where(p => p.JiraKey == subTask.Key);

                var subTaskWorkItem = GetDevWorkItemView(subTask, subTaskPullRequest);

                workItem.SubTasks.Add(subTaskWorkItem);
            }

            return workItem;
        }

        private WorkItemStatus GetWorkItemStatus(JiraIssue jiraIssue, IEnumerable<GitHubPullRequest> pullRequests)
        {
            WorkItemStatus status = jiraIssue.WorkItemStatus;

            if (pullRequests.Count() > 0)
            {
                status |= WorkItemStatus.PullRequestCreated;

                if (pullRequests.Any(p => p.HasBeenReviewed))
                {
                    status |= WorkItemStatus.PullRequestReviewed;
                }
                if (pullRequests.Any(p => p.HasRequestedChanges))
                {
                    status |= WorkItemStatus.PullRequestChangesRequested;
                }
                if (pullRequests.Any(p => p.IsApproved))
                {
                    status |= WorkItemStatus.PullRequestApproved;
                }
                if (pullRequests.Any(p => p.IsReadyForQA))
                {
                    status |= WorkItemStatus.ReadyForQA;
                }
            }

            return status;
        }

        private void ClearWorkItems()
        {
            dgvWorkItems.DataSource = null;
        }

        private void ClearSubTasks()
        {
            dgvSubTasks.DataSource = null;
        }

        private void ClearPullRequests()
        {
            dgvPullRequests.DataSource = null;
        }

        private void DisplayDevWorkItemViews(IList<DevWorkItemView> workItems)
        {
            dgvWorkItems.DataSource = null;

            dgvWorkItems.DataSource = workItems;

            if (workItems.Count > 0)
            {
                dgvWorkItems.Rows[0].Selected = true;
            }
        }

        private void DisplaySubTasks(IList<DevWorkItemView> subTasks)
        {
            if (subTasks != null)
            {
                dgvSubTasks.DataSource = subTasks;

                if (subTasks.Count > 0)
                {
                    dgvSubTasks.Rows[0].Selected = true;
                }
            }
        }

        private void DisplayPullRequests(IList<GitHubPullRequest> pullRequests)
        {
            if (pullRequests != null)
            {
                dgvPullRequests.DataSource = pullRequests;
            }
        }

        #endregion
    }
}
