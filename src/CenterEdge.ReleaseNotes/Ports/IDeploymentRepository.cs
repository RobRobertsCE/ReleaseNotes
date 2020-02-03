using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CenterEdge.ReleaseNotes.Models;

namespace CenterEdge.ReleaseNotes.Ports
{
    public interface IDeploymentRepository
    {
        Task<Deployment> GetAsync(Version version);
        Task<IEnumerable<Deployment>> GetListAsync();
        Task<IEnumerable<Deployment>> GetRangeAsync(Version start, Version end);
        Task<Deployment> InsertAsync(Deployment deployment);
        Task<Deployment> UpdateAsync(Deployment deployment);
        Task DeleteAsync(Version version);
    }
}
