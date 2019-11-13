using System;
using System.Collections.Generic;
using System.Text;

namespace CenterEdge.SourceControl.Models
{
    public class ReleaseTag
    {
        public string Name { get; set; }
        public Version Release { get; set; }
        public bool IsPatch { get; set; }
        public string CommitSha { get; set; }
        public string CommitRef { get; set; }

        public override string ToString()
        {
            return $"[{Release}] {Name} IsPatch:{IsPatch} CommitSha:{CommitSha} CommitRef:{CommitRef}";
        }
    }
}
