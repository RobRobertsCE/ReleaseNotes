using System;
using System.Collections.Generic;
using System.Text;

namespace CenterEdge.SourceControl.Models
{
    public class ReleaseItem
    {
        public string Name { get; set; }
        public Version Release { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public override string ToString()
        {
            return $"[{Release}] {Name} {CreatedAt}";
        }
    }
}
