using System;

namespace CenterEdge.ReleaseNotes.Models
{
    public class Commit
    {
        public string Id { get; set; }
        public DateTime MergeTimestamp { get; set; }
        public string IssueKey { get; set; }
        public string CommitMessage { get; set; }
        public ReleaseScope ReleaseScope { get; set; }
        public string ReleaseNote { get; set; }

        internal void UpdateFrom(Commit commit)
        {
            if (commit == null)
                throw new ArgumentNullException(nameof(commit));

            if (commit.Id != Id)
                throw new ArgumentException($"Id mismatch updating commit. Current:{Id}, New:{commit.Id}");

            MergeTimestamp = commit.MergeTimestamp;
            IssueKey = commit.IssueKey;
            CommitMessage = commit.CommitMessage;
            ReleaseScope = commit.ReleaseScope;
            ReleaseNote = commit.ReleaseNote;
        }
    }
}
