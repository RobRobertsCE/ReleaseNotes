namespace CenterEdge.SourceControl.GitHub.Data
{
    public class GitHubOptions
    {
        public const string GitHubConfigurationSection = "GitHubOptions";

        public string companyName { get; set; }
        public string repoName { get; set; }
        public string token { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
