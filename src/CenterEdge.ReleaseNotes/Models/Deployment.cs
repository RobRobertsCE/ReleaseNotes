using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CenterEdge.ReleaseNotes.Models
{
    public class Deployment
    {
        public Version Version { get; set; }
        public bool IsLts { get; set; }
        public bool IsPatch { get; set; }
        public bool IsDeployed { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? BuildDate { get; set; }
        public DateTime? DeployDate { get; set; }
        public IList<Issue> Issues { get; set; } = new List<Issue>();

        internal void UpdateFrom(Deployment deployment)
        {
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));

            if (deployment.Version != Version)
                throw new ArgumentException($"Version mismatch updating deployment. Current:{Version}, New:{deployment.Version}");

            IsLts = deployment.IsLts;
            IsPatch = deployment.IsPatch;
            IsDeployed = deployment.IsDeployed;
            Notes = deployment.Notes;
            StartDate = deployment.StartDate;
            BuildDate = deployment.BuildDate;
            DeployDate = deployment.DeployDate;

            var matches = Issues.Join(
                deployment.Issues,
                o => o.IssueKey,
                n => n.IssueKey,
                (o, n) => new { Original = o, New = n });

            foreach (var itemToUpdate in matches)
            {
                itemToUpdate.Original.UpdateFrom(itemToUpdate.New);
            }

            foreach (var itemToRemove in Issues.Except(deployment.Issues).ToList())
            {
                Issues.Remove(itemToRemove);
            }

            foreach (var itemToAdd in deployment.Issues.Except(Issues).ToList())
            {
                Issues.Add(itemToAdd);
            }
        }
    }
}
