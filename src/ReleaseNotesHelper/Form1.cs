using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CenterEdge.ReleaseNotes;
using CenterEdge.ReleaseNotes.Models;
using CenterEdge.ReleaseNotes.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace ReleaseNotesHelper
{
    public partial class Form1 : Form
    {
        #region fields

        IServiceProvider _serviceProvider;

        #endregion

        #region ctor / load

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _serviceProvider = BuildServices();

                await RefreshDeploymentsListAsync();
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        #endregion

        #region private

        // common
        private void ExceptionHandler(Exception ex)
        {
            MessageBox.Show(ex.Message);
            Console.WriteLine(ex.ToString());
        }

        private IServiceProvider BuildServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddReleaseNotes();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        // load and display deployments
        private async Task RefreshDeploymentsListAsync()
        {
            var deployments = await GetDeploymentsAsync();

            DisplayDeployments(deployments);
        }

        private async Task<IList<Deployment>> GetDeploymentsAsync()
        {
            var deploymentsRepository = _serviceProvider.GetRequiredService<IDeploymentRepository>();

            var deployments = await deploymentsRepository.GetListAsync();

            return deployments.ToList();
        }

        private void DisplayDeployments(IList<Deployment> deployments)
        {
            listBox1.DataSource = null;

            listBox1.DisplayMember = "Version";
            listBox1.ValueMember = "Version";

            listBox1.DataSource = deployments.OrderBy(d => d.Version).ToList();
        }

        // event handlers
        private async void btnNewDeployment_Click(object sender, EventArgs e)
        {
            try
            {
                var repo = _serviceProvider.GetRequiredService<IDeploymentRepository>();

                var deployments = await repo.GetListAsync();

                var next = 1;

                if (deployments.Count() > 0)
                    next = deployments.OrderBy(d => d.Version).LastOrDefault().Version.Major + 1;

                var deployment = new Deployment()
                {
                    Version = new Version(next, 2, 3, 4),
                    IsDeployed = false,
                    IsLts = false,
                    IsPatch = false,
                    BuildDate = DateTime.Now,
                    StartDate = DateTime.Now.AddDays(-30),
                    Notes = "First!!1!11"
                };

                var issue = new Issue()
                {
                    IssueKey = "ADV-123",
                    Type = IssueType.UserStory
                };

                var commit = new Commit()
                {
                    Id = "br459",
                    IssueKey = issue.IssueKey,
                    ReleaseScope = ReleaseScope.All,
                    MergeTimestamp = DateTime.Now.AddDays(-25),
                    CommitMessage = "Do the thing! (ADV-123)"
                };

                issue.Commits.Add(commit);

                deployment.Issues.Add(issue);

                await repo.InsertAsync(deployment);

                await RefreshDeploymentsListAsync();
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewSelector_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
