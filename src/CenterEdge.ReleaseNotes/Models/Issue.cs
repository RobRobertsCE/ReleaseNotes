using System;
using System.Collections.Generic;
using System.Linq;

namespace CenterEdge.ReleaseNotes.Models
{
    public class Issue
    {
        public string IssueKey { get; set; }
        public IssueType Type { get; set; }
        public string Description { get; set; }
        public IList<Commit> Commits { get; set; } = new List<Commit>();

        internal void UpdateFrom(Issue issue)
        {
            if (issue == null)
                throw new ArgumentNullException(nameof(issue));

            if (issue.IssueKey != IssueKey)
                throw new ArgumentException($"IssueKey mismatch updating issue. Current:{IssueKey}, New:{issue.IssueKey}");

            Type = issue.Type;
            Description = issue.Description;

            var matches = Commits.Join(
                issue.Commits,
                o => o.Id,
                n => n.Id,
                (o, n) => new { Original = o, New = n });

            foreach (var itemToUpdate in matches)
            {
                itemToUpdate.Original.UpdateFrom(itemToUpdate.New);
            }

            foreach (var itemToRemove in Commits.Except(issue.Commits).ToList())
            {
                Commits.Remove(itemToRemove);
            }

            foreach (var itemToAdd in issue.Commits.Except(Commits).ToList())
            {
                Commits.Add(itemToAdd);
            }
        }
    }
}
