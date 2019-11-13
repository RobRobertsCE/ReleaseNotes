using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Atlassian.Jira;
using CenterEdge.JiraLibrary.Models;
using CenterEdge.JiraLibrary.Ports;

namespace CenterEdge.JiraLibrary.Adapters
{
    internal class JiraRepository : IJiraRepository
    {
        public JiraRepository()
        {

        }


        public async Task<JiraIssue> GetJiraIssueAsync(string key)
        {
            //// create a connection to JIRA using the Rest client
            var jira = Jira.CreateRestClient("https://centeredge.atlassian.net", "rroberts", "hel-j205");
            //var jira = GetJiraClient();

            var issue = await jira.Issues.GetIssueAsync(key);

            //// use LINQ syntax to retrieve issues
            //var issues = from i in jira.Issues.Queryable
            //             where i.Assignee == "admin" && i.Priority == "Major"
            //             orderby i.Created
            //             select i;

            return new JiraIssue()
            {
                Key = key,
                Type = issue?.Type.Name
            };
        }

        public async Task<IList<JiraIssue>> GetJiraIssuesAsync()
        {
            var jiraIssues = new List<JiraIssue>();

            //// create a connection to JIRA using the Rest client
            var jira = Jira.CreateRestClient("https://centeredge.atlassian.net", "rroberts", "hel-j205");
            //var jira = GetJiraClient();

            var projects = await jira.Projects.GetProjectsAsync();

            foreach (var project in projects)
            {
                System.Console.WriteLine(project.Name);
            }

            ////// use LINQ syntax to retrieve issues
            //var issues = await jira.Issues.GetIssuesAsync(new string[] { "ADV-15109" });

            //foreach (var issue in issues)
            //{
            //    jiraIssues.Add(new JiraIssue()
            //    {
            //        Key = issue.Key,
            //        Type = issue.Value.Type.Name
            //    });
            //}

            return jiraIssues;
        }

        private Jira GetJiraClient()
        {
            var jiraUrl = "https://centeredge.atlassian.net";

            JiraCredentials credentials = new JiraCredentials("rroberts", "0PhAkTvu7crpypNWG5dM8932");

            var client = Atlassian.Jira.Jira.CreateRestClient(jiraUrl);

            var privateKey = "0PhAkTvu7crpypNWG5dM8932"; // <- get your private key in here
            var decoder = new OpenSSL.PrivateKeyDecoder.OpenSSLPrivateKeyDecoder();
            var keyInfo = decoder.Decode(privateKey);
            var consumerSecret = keyInfo.ToXmlString(true);// OpenSSL.PrivateKeyDecoder.RSAExtensions.ToXmlString(keyInfo, true);

            string consumerKey = "";
            //string consumerSecret = "";
            string accessToken = "";
            string accessTokenSecret = "";

            client.RestClient.RestSharpClient.Authenticator =
                RestSharp.Authenticators.OAuth1Authenticator.ForProtectedResource(
                    consumerKey,        // <- fill it
                    consumerSecret,     // <- generated
                    accessToken,        // <- fill it
                    accessTokenSecret,  // <- fill it
                    RestSharp.Authenticators.OAuth.OAuthSignatureMethod.RsaSha1
                );

            return client;
        }
    }
}
