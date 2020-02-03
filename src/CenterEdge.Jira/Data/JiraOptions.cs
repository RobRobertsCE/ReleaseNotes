namespace CenterEdge.JiraLibrary.Data
{
    public class JiraOptions
    {

        public const string JiraConfigurationSection = "JiraOptions";

        public string companyName { get; set; }
        public string projectName { get; set; }
        public string projectKey { get; set; }
        public string team { get; set; }
        public string apiKey { get; set; }
        public string userEMail { get; set; }
    }
}
