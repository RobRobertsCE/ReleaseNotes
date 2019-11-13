using System;
using System.Collections.Generic;
using System.Text;

namespace CenterEdge.JiraLibrary.Data
{
    public class JiraOptions
    {

        public const string JiraConfigurationSection = "JiraOptions";

        public string companyName { get; set; }
        public string repoName { get; set; }
        public string token { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
