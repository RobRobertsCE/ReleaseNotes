using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CenterEdge.JiraLibrary;
using CenterEdge.JiraLibrary.Ports;
using CenterEdge.SourceControl.GitHub;
using CenterEdge.SourceControl.Models;
using CenterEdge.SourceControl.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CenterEdge.SourceControl.TestApp
{
    public partial class Form1 : Form
    {
        IServiceProvider _provider;
        IList<ReleaseTag> _releaseTags;
        bool _loading = true;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _provider = GetServiceProvider();

                //var jira = _provider.GetRequiredService<IJiraRepository>();
                
                //var issue = await jira.GetJiraIssueAsync("ADV-15109");

                //var issues = await jira.GetJiraIssuesAsync();

                await LoadReleaseTagsAsync();

                _loading = false;

                pnlControls.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadReleaseTagsAsync()
        {
            IWorkItemRepository workItemRepository = _provider.GetRequiredService<IWorkItemRepository>();

            _releaseTags = await workItemRepository.GetReleaseTagsAsync();

            lstStartRelease.DataSource = null;
            lstStartRelease.DisplayMember = "Name";
            lstStartRelease.DataSource = _releaseTags.OrderByDescending(r => r.Release).ToList();

            lstStartRelease.SelectedIndex = -1;
        }

        private void LstStartRelease_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            lstEndRelease.DataSource = null;

            if (lstStartRelease.SelectedItem == null)
                return;

            ReleaseTag selectedRelease = lstStartRelease.SelectedItem as ReleaseTag;

            btnSelectedRelease.Enabled = (selectedRelease != null);

            if (selectedRelease == null)
                return;

            lstEndRelease.DisplayMember = "Name";
            lstEndRelease.DataSource = _releaseTags.Where(r => r.Release >= selectedRelease.Release).OrderByDescending(r => r.Release).ToList();

            lstEndRelease.SelectedIndex = -1;
        }

        private void lstEndRelease_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
                return;

            btnSelectedRange.Enabled = (lstStartRelease.SelectedItem != null && lstEndRelease.SelectedItem != null);
        }

        private async void btnSelectedRange_Click(object sender, EventArgs e)
        {
            try
            {
                BeginSearch();

                await LoadSelectedRangeReleaseNotesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SearchComplete();
            }
        }

        private async void btnSelectedRelease_Click(object sender, EventArgs e)
        {
            try
            {
                BeginSearch();

                await LoadSelectedReleaseNotesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SearchComplete();
            }
        }

        private async void btnCurrentRelease_Click(object sender, EventArgs e)
        {
            try
            {
                BeginSearch();

                await LoadCurrentReleaseNotesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SearchComplete();
            }
        }

        private async Task LoadCurrentReleaseNotesAsync()
        {
            IWorkItemRepository workItemRepository = _provider.GetRequiredService<IWorkItemRepository>();

            IList<WorkItem> workItems = await workItemRepository.GetLatestReleaseWorkItemsAsync();

            DisplayWorkItems(workItems);
        }
        private async Task LoadSelectedReleaseNotesAsync()
        {
            IWorkItemRepository workItemRepository = _provider.GetRequiredService<IWorkItemRepository>();

            ReleaseTag releaseTag = lstStartRelease.SelectedItem as ReleaseTag;

            IList<WorkItem> workItems = await workItemRepository.GetReleaseWorkItemsAsync(releaseTag);

            DisplayWorkItems(workItems);
        }

        private async Task LoadSelectedRangeReleaseNotesAsync()
        {
            IWorkItemRepository workItemRepository = _provider.GetRequiredService<IWorkItemRepository>();

            ReleaseTag @base = lstStartRelease.SelectedItem as ReleaseTag;
            ReleaseTag head = lstEndRelease.SelectedItem as ReleaseTag;

            IList<WorkItem> workItems = await workItemRepository.GetReleaseWorkItemsInRangeAsync(@base, head);

            DisplayWorkItems(workItems);
        }

        private void DisplayWorkItems(IList<WorkItem> workItems)
        {
            txtReleaseNotes.Clear();

            if (workItems.Count == 0)
                return;

            Version currentRelease = new Version();

            foreach (WorkItem workItem in workItems.OrderByDescending(r => r.Release))
            {
                if (workItem.Release != currentRelease)
                {
                    txtReleaseNotes.AppendText($"# {workItem.Release} Release Notes\r\n\r\n");
                    currentRelease = workItem.Release;
                }

                string message = String.IsNullOrEmpty(workItem.Motivation) ? "\r\n" : workItem.Motivation.EndsWith("\r\n") ? workItem.Motivation.Substring(0, workItem.Motivation.Length - 2) : workItem.Motivation;

                txtReleaseNotes.AppendText($"* [{workItem.JiraTag}] ({workItem.JiraUrl}): {workItem.Title} {message}\r\n");
            }
        }

        private void BeginSearch()
        {
            pnlControls.Enabled = false;
            txtReleaseNotes.Enabled = false;
            txtReleaseNotes.Clear();
        }

        private void SearchComplete()
        {
            pnlControls.Enabled = true;
            txtReleaseNotes.Enabled = true;
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
    }
}
