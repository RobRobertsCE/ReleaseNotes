using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CenterEdge.SourceControl.GitHub.Data;
using CenterEdge.SourceControl.Models;
using CenterEdge.SourceControl.Ports;
using Microsoft.Extensions.Options;
using MoreLinq;
using Octokit;

namespace CenterEdge.SourceControl.GitHub.Adapters
{
    internal class WorkItemRepository : IWorkItemRepository
    {
        #region consts

        private const string IssueTrackerUrlToken = "https://centeredge.atlassian.net/browse/";

        #endregion

        #region fields

        private readonly IOptions<GitHubOptions> _gitHubOptions;

        #endregion

        #region ctor

        public WorkItemRepository(IOptions<GitHubOptions> gitHubOptions)
        {
            _gitHubOptions = gitHubOptions ?? throw new ArgumentNullException(nameof(gitHubOptions));
        }

        #endregion

        #region public

        public async Task<IList<ReleaseTag>> GetReleaseTagsAsync()
        {
            Repository gitHubRepo = await GetRepositoryAsync();

            return await GetReleaseTagsAsync(gitHubRepo.Id);
        }
        public async Task<IList<ReleaseTag>> GetReleaseTagsAsync(long repositoryId)
        {
            IList<ReleaseTag> releases = new List<ReleaseTag>();

            IReadOnlyList<RepositoryTag> tags = null;

            GitHubClient client = GetClient();

            ApiOptions apiOptions = new ApiOptions()
            {
                PageCount = 1,
                PageSize = 20,
                StartPage = 1
            };

            tags = await client.Repository.GetAllTags(repositoryId, apiOptions);

            while (tags.Count > 0)
            {
                foreach (RepositoryTag tag in tags)
                {
                    Version versionBuffer = GetVersionFromTag(tag.Name);

                    releases.Add(new ReleaseTag()
                    {
                        Name = tag.Name,
                        Release = GetVersionFromTag(tag.Name),
                        CommitSha = tag.Commit.Sha,
                        CommitRef = tag.Commit.Ref,
                        IsPatch = tag.Name.Contains("patch")
                    });
                }

                apiOptions.StartPage++;

                tags = await client.Repository.GetAllTags(repositoryId, apiOptions);
            }

            return releases;
        }

        public async Task<IList<WorkItem>> GetLatestReleaseWorkItemsAsync()
        {
            Repository gitHubRepo = await GetRepositoryAsync();

            return await GetLatestReleaseWorkItemsAsync(gitHubRepo.Id);
        }
        public async Task<IList<WorkItem>> GetLatestReleaseWorkItemsAsync(long repositoryId)
        {
            IList<WorkItem> workItems = new List<WorkItem>();

            GitHubClient client = GetClient();

            var advantageReleaseTags = await GetReleaseTagsAsync(repositoryId);

            var mostRecentTwoReleases = advantageReleaseTags
                .Where(r => r.IsPatch == false)
                .OrderByDescending(r => r.Release)
                .Take(2)
                .ToArray();

            var head = mostRecentTwoReleases[0];
            var @base = mostRecentTwoReleases[1];

            workItems = await GetWorkItemsInRangeAsync(client, repositoryId, @base, head);

            return workItems;
        }

        public async Task<IList<WorkItem>> GetReleaseWorkItemsInRangeAsync(ReleaseTag @base, ReleaseTag head)
        {
            Repository gitHubRepo = await GetRepositoryAsync();

            return await GetReleaseWorkItemsInRangeAsync(gitHubRepo.Id, @base, head);
        }
        public async Task<IList<WorkItem>> GetReleaseWorkItemsInRangeAsync(long repositoryId, ReleaseTag @base, ReleaseTag head)
        {
            GitHubClient client = GetClient();

            return await GetWorkItemsInRangeAsync(client, repositoryId, @base, head);
        }

        public async Task<IList<WorkItem>> GetReleaseWorkItemsAsync(ReleaseTag releaseTag)
        {
            Repository gitHubRepo = await GetRepositoryAsync();

            return await GetReleaseWorkItemsAsync(gitHubRepo.Id, releaseTag);
        }
        public async Task<IList<WorkItem>> GetReleaseWorkItemsAsync(long repositoryId, ReleaseTag releaseTag)
        {
            GitHubClient client = GetClient();

            return await GetWorkItemsInReleaseAsync(client, repositoryId, releaseTag);
        }

        public async Task<IList<GitHubPullRequest>> GetPullRequestsAsync()
        {
            IList<GitHubPullRequest> openPullRequests = new List<GitHubPullRequest>();

            Repository gitHubRepo = await GetRepositoryAsync();

            GitHubClient client = GetClient();

            var pullRequests = await client.Repository.PullRequest.GetAllForRepository(gitHubRepo.Id);

            foreach (PullRequest pr in pullRequests)
            {
                var pullRequest = ToGitHubPullRequest(pr);

                var reviews = await client.PullRequest.Review.GetAll(gitHubRepo.Id, pr.Number);

                if (reviews.Any())
                {
                    pullRequest.HasBeenReviewed = true;
                }

                if (reviews.Any(r => r.State == "Commented"))
                {
                    pullRequest.HasRequestedChanges = true;
                }

                if (reviews.Any(r => r.State == "Approved"))
                {
                    pullRequest.IsApproved = true;
                }

                openPullRequests.Add(pullRequest);
            }

            return openPullRequests;
        }

        #endregion

        #region protected

        protected virtual IList<GitHubPullRequest> ToGitHubPullRequests(IReadOnlyList<PullRequest> pullRequests)
        {
            IList<GitHubPullRequest> openPullRequests = new List<GitHubPullRequest>();

            foreach (PullRequest pullRequest in pullRequests)
            {
                var openPullRequest = new GitHubPullRequest()
                {
                    Id = pullRequest.Id,
                    Title = pullRequest.Title,
                    Status = pullRequest.State.ToString(),
                    Number = pullRequest.Number,
                    JiraKey = GetJiraKeyFromTitle(pullRequest.Title),
                    IsReadyForQA = pullRequest.Labels.Any(l => l.Name == "Ready For QA"),
                    IsPatch = pullRequest.Labels.Any(l => l.Name == "Patch Included"),
                    IsOnHold = pullRequest.Labels.Any(l => l.Name == "On Hold")
                };

                openPullRequests.Add(openPullRequest);
            }

            return openPullRequests;
        }
        protected virtual GitHubPullRequest ToGitHubPullRequest(PullRequest pullRequest)
        {
            var openPullRequest = new GitHubPullRequest()
            {
                Id = pullRequest.Id,
                Title = pullRequest.Title,
                Status = pullRequest.State.ToString(),
                Number = pullRequest.Number,
                JiraKey = GetJiraKeyFromTitle(pullRequest.Title),
                IsReadyForQA = pullRequest.Labels.Any(l => l.Name == "Ready For QA"),
                IsPatch = pullRequest.Labels.Any(l => l.Name == "Patch Included"),
                IsOnHold = pullRequest.Labels.Any(l => l.Name == "On Hold")
            };

            return openPullRequest;
        }
        protected virtual WorkItem GetWorkItemFromGitHubCommit(GitHubCommit commit, Version release)
        {
            WorkItem workItem = new WorkItem()
            {
                Id = commit.Sha,
                Release = release,
                Message = commit.Commit.Message
            };

            var workItemMessageLines = commit.Commit.Message.Split('\n');

            workItem.Title = workItemMessageLines.FirstOrDefault().Replace("\n", "");

            string currentToken = String.Empty;
            foreach (string workItemMessageLine in workItemMessageLines.Skip(1))
            {
                if (workItemMessageLine.StartsWith("---"))
                {
                    continue;
                }
                else if (workItemMessageLine.StartsWith(IssueTrackerUrlToken))
                {
                    workItem.JiraUrl = workItemMessageLine.Replace("\n", "");
                    workItem.JiraTag = workItem.JiraUrl.Substring(IssueTrackerUrlToken.Length);
                    continue;
                }
                else if (workItemMessageLine.StartsWith("Motivation"))
                {
                    currentToken = "Motivation";
                    continue;
                }
                else if (workItemMessageLine.StartsWith("Modification"))
                {
                    currentToken = "Modifications";
                    continue;
                }
                else if (workItemMessageLine.StartsWith("Actions"))
                {
                    currentToken = "Actions";
                    continue;
                }
                else if (workItemMessageLine.StartsWith("Results"))
                {
                    currentToken = "Results";
                    continue;
                }

                if (currentToken == "Motivation")
                {
                    workItem.Motivation += workItemMessageLine + "\r\n";
                }
                else if (currentToken == "Modifications")
                {
                    workItem.Modifications += workItemMessageLine + "\r\n";
                }
                else if (currentToken == "Actions")
                {
                    workItem.Actions += workItemMessageLine + "\r\n";
                }
                else if (currentToken == "Results")
                {
                    workItem.Results += workItemMessageLine + "\r\n";
                }
            }

            return workItem;
        }

        protected virtual Version GetVersionFromTag(string tagName)
        {
            Version result;

            string prefixRemoved = tagName.StartsWith("v") ?
                tagName.Substring(1) :
                tagName;

            string sectionsRemoved = prefixRemoved.Contains("-") ?
                prefixRemoved.Split('-')[0] :
                prefixRemoved;

            if (Version.TryParse(sectionsRemoved, out result))
            {
                return result;
            }

            Console.WriteLine($"**** Could not parse {tagName} ***");

            return new Version(0, 0);
        }

        protected virtual GitHubClient GetClient()
        {
            var identity = $"{_gitHubOptions.Value.companyName}";
            //var productInformation = new ProductHeaderValue(identity);
            //var credentials = new Credentials(_gitHubOptions.Value.token);
            //var client = new GitHubClient(productInformation) { Credentials = credentials };

            var username = "robrobertsce";
            var password = _gitHubOptions.Value.token;// "hel-j205";
            var productInformation = new ProductHeaderValue(identity);
            var credentials = new Credentials(username, password, AuthenticationType.Basic);

            return new GitHubClient(productInformation) { Credentials = credentials };
        }

        protected virtual async Task<Repository> GetRepositoryAsync()
        {
            GitHubClient client = GetClient();

            return await client.Repository.Get(_gitHubOptions.Value.companyName, _gitHubOptions.Value.repoName);
        }

        protected virtual async Task<Branch> GetMasterBranchAsync()
        {
            GitHubClient client = GetClient();

            return await client.Repository.Branch.Get(_gitHubOptions.Value.companyName, _gitHubOptions.Value.repoName, "master");
        }

        protected virtual async Task<IList<WorkItem>> GetWorkItemsInRangeAsync(GitHubClient client, long repositoryId, ReleaseTag @base, ReleaseTag head)
        {
            IList<WorkItem> workItems = new List<WorkItem>();
            CompareResult commits = null;

            var advantageReleaseTags = await GetReleaseTagsAsync(repositoryId);

            var selectedReleaseTags = advantageReleaseTags.
                Where(r => r.Release >= @base.Release && r.Release <= head.Release && r.IsPatch == false).
                OrderByDescending(r => r.Release).
                ToList();

            if (selectedReleaseTags.Count < 2)
                return workItems;

            string baseSha = selectedReleaseTags[1].CommitSha;
            string headSha = selectedReleaseTags[0].CommitSha;

            commits = await client.Repository.Commit.Compare
            (
                repositoryId,
                baseSha,
                headSha
            );

            foreach (GitHubCommit commit in commits.Commits)
            {
                workItems.Add(GetWorkItemFromGitHubCommit(commit, selectedReleaseTags[0].Release));
            }

            for (int i = 1; i < selectedReleaseTags.Count() - 1; i++)
            {
                var selectedBase = selectedReleaseTags[i + 1];
                var selectedHead = selectedReleaseTags[i];

                commits = await client.Repository.Commit.Compare
                (
                    repositoryId,
                    selectedBase.CommitSha,
                    selectedHead.CommitSha
                );

                foreach (GitHubCommit commit in commits.Commits)
                {
                    workItems.Add(GetWorkItemFromGitHubCommit(commit, selectedHead.Release));
                }
            }

            return workItems;
        }

        protected virtual async Task<IList<WorkItem>> GetWorkItemsInReleaseAsync(GitHubClient client, long repositoryId, ReleaseTag releaseTag)
        {
            IList<WorkItem> workItems = new List<WorkItem>();
            CompareResult commits = null;

            var advantageReleaseTags = await GetReleaseTagsAsync(repositoryId);

            var orderedReleaseTags = advantageReleaseTags.
                OrderByDescending(r => r.Release).
                ToList();

            var selectedReleaseIndex = orderedReleaseTags.FindIndex(r => r.Release == releaseTag.Release);

            string baseSha = String.Empty;
            string headSha = String.Empty;
            Version releaseVersion = null;

            if (selectedReleaseIndex == -1)
            {
                // Not found
                return workItems;
            }
            else if (selectedReleaseIndex == orderedReleaseTags.Count)
            {
                // Latest release
                //baseSha = orderedReleaseTags[selectedReleaseIndex-1].CommitSha;
                //headSha = orderedReleaseTags[selectedReleaseIndex].CommitSha;
                //releaseVersion = orderedReleaseTags[selectedReleaseIndex - 1].Release;
            }
            else
            {
                baseSha = orderedReleaseTags[selectedReleaseIndex].CommitSha;
                headSha = orderedReleaseTags[selectedReleaseIndex - 1].CommitSha;
                releaseVersion = orderedReleaseTags[selectedReleaseIndex].Release;
            }

            commits = await client.Repository.Commit.Compare
            (
                repositoryId,
                baseSha,
                headSha
            );

            foreach (GitHubCommit commit in commits.Commits)
            {
                workItems.Add(GetWorkItemFromGitHubCommit(commit, releaseVersion));
            }

            //for (int i = 1; i < selectedReleaseTags.Count() - 1; i++)
            //{
            //    var selectedBase = selectedReleaseTags[i + 1];
            //    var selectedHead = selectedReleaseTags[i];

            //    commits = await client.Repository.Commit.Compare
            //    (
            //        repositoryId,
            //        selectedBase.CommitSha,
            //        selectedHead.CommitSha
            //    );

            //    foreach (GitHubCommit commit in commits.Commits)
            //    {
            //        workItems.Add(GetWorkItemFromGitHubCommit(commit, selectedHead.Release));
            //    }
            //}

            return workItems;
        }

        #endregion

        #region private

        private string GetJiraKeyFromTitle(string title)
        {
            return title.Substring(title.LastIndexOf('(') + 1).TrimEnd(')');
        }

        #endregion
    }
}
