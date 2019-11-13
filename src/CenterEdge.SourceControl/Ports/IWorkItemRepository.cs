using System.Collections.Generic;
using System.Threading.Tasks;
using CenterEdge.SourceControl.Models;

namespace CenterEdge.SourceControl.Ports
{
    public interface IWorkItemRepository
    {
        Task<IList<ReleaseTag>> GetReleaseTagsAsync();
        Task<IList<ReleaseItem>> GetReleasesAsync();
        Task<IList<WorkItem>> GetLatestReleaseWorkItemsAsync();
        Task<IList<WorkItem>> GetReleaseWorkItemsAsync(ReleaseTag releaseTag);
        Task<IList<WorkItem>> GetReleaseWorkItemsInRangeAsync(ReleaseTag @base, ReleaseTag head);
    }
}
