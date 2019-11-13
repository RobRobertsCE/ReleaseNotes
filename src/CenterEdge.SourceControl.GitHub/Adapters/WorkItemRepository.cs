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
        private readonly IOptions<GitHubOptions> _gitHubOptions;
        private static string _urlToken = "https://centeredge.atlassian.net/browse/";

        public WorkItemRepository(IOptions<GitHubOptions> gitHubOptions)
        {
            _gitHubOptions = gitHubOptions ?? throw new ArgumentNullException(nameof(gitHubOptions));
        }

        public async Task<IList<ReleaseTag>> GetReleaseTagsAsync()
        {
            Repository gitHubRepo = await GetRepositoryAsync();

            return await GetReleaseTagsAsync(gitHubRepo.Id);
        }
        public async Task<IList<ReleaseTag>> GetReleaseTagsAsync(long repositoryId)
        {
            IList<ReleaseTag> releases = new List<ReleaseTag>();

            try
            {
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
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
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

            try
            {
                GitHubClient client = GetClient();

                var advantageReleaseTags = await GetReleaseTagsAsync(repositoryId);

                var mostRecentTwoReleases = advantageReleaseTags
                    .Where(r => r.IsPatch == false)
                    .OrderByDescending(r => r.Release)
                    .Take(2)
                    .ToArray();

                var head = mostRecentTwoReleases[0];
                var @base = mostRecentTwoReleases[1];

                return await GetWorkItemsInRangeAsync(client, repositoryId, @base, head);
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }

            return workItems;
        }

        public async Task<IList<WorkItem>> GetReleaseWorkItemsInRangeAsync(ReleaseTag @base, ReleaseTag head)
        {
            Repository gitHubRepo = await GetRepositoryAsync();

            return await GetReleaseWorkItemsInRangeAsync(gitHubRepo.Id, @base, head);
        }
        public async Task<IList<WorkItem>> GetReleaseWorkItemsInRangeAsync(long repositoryId, ReleaseTag @base, ReleaseTag head)
        {
            IList<WorkItem> workItems = new List<WorkItem>();

            try
            {
                GitHubClient client = GetClient();

                return await GetWorkItemsInRangeAsync(client, repositoryId, @base, head);
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }

            return workItems;
        }

        public async Task<IList<WorkItem>> GetReleaseWorkItemsAsync(ReleaseTag releaseTag)
        {
            Repository gitHubRepo = await GetRepositoryAsync();

            return await GetReleaseWorkItemsAsync(gitHubRepo.Id, releaseTag);
        }
        public async Task<IList<WorkItem>> GetReleaseWorkItemsAsync(long repositoryId, ReleaseTag releaseTag)
        {
            IList<WorkItem> workItems = new List<WorkItem>();

            try
            {
                GitHubClient client = GetClient();

                return await GetWorkItemsInReleaseAsync(client, repositoryId, releaseTag);
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }

            return workItems;
        }

        public async Task<IList<ReleaseItem>> GetReleasesAsync()
        {
            Repository gitHubRepo = await GetRepositoryAsync();

            return await GetReleasesAsync(gitHubRepo.Id);
        }
        public async Task<IList<ReleaseItem>> GetReleasesAsync(long repositoryId)
        {
            IList<ReleaseItem> releaseItems = new List<ReleaseItem>();

            try
            {
                IReadOnlyList<Release> releases = null;

                GitHubClient client = GetClient();

                ApiOptions apiOptions = new ApiOptions()
                {
                    PageCount = 1,
                    PageSize = 20,
                    StartPage = 1
                };

                releases = await client.Repository.Release.GetAll(repositoryId, apiOptions);

                while (releases.Count > 0)
                {
                    foreach (Release release in releases)
                    {
                        Version versionBuffer = GetVersionFromTag(release.Name);

                        releaseItems.Add(new ReleaseItem()
                        {
                            Name = release.Name,
                            Release = GetVersionFromTag(release.Name),
                            CreatedAt = release.CreatedAt
                        });
                    }

                    apiOptions.StartPage++;

                    releases = await client.Repository.Release.GetAll(repositoryId, apiOptions);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }

            return releaseItems;
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
                else if (workItemMessageLine.StartsWith(_urlToken))
                {
                    workItem.JiraUrl = workItemMessageLine.Replace("\n", "");
                    workItem.JiraTag = workItem.JiraUrl.Substring(_urlToken.Length);
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

        protected virtual void ExceptionHandler(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        protected virtual GitHubClient GetClient()
        {
            var identity = $"{_gitHubOptions.Value.companyName}";
            var productInformation = new ProductHeaderValue(identity);
            var credentials = new Credentials(_gitHubOptions.Value.token);
            var client = new GitHubClient(productInformation) { Credentials = credentials };

            return client;
        }

        protected virtual async Task<Repository> GetRepositoryAsync()
        {
            GitHubClient client = GetClient();

            Repository repository = await client.Repository.Get(_gitHubOptions.Value.companyName, _gitHubOptions.Value.repoName);

            return repository;
        }

        protected virtual async Task<Branch> GetMasterBranchAsync()
        {
            GitHubClient client = GetClient();

            Branch masterBranch = await client.Repository.Branch.Get(_gitHubOptions.Value.companyName, _gitHubOptions.Value.repoName, "master");

            return masterBranch;
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
    }
}
