using System;
using System.Collections.Generic;
using System.Text;

namespace CenterEdge.JiraLibrary.Models
{
    public class JiraBaseItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}
