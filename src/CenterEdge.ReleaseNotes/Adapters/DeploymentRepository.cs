using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CenterEdge.ReleaseNotes.Internal;
using CenterEdge.ReleaseNotes.Models;
using CenterEdge.ReleaseNotes.Ports;

namespace CenterEdge.ReleaseNotes.Adapters
{
    internal class DeploymentRepository : JsonRepository<Deployment>, IDeploymentRepository
    {
        #region fields

        private IList<Deployment> _deployments;

        #endregion

        #region properties

        protected internal override string DataFile { get; set; } = "deployments.json";

        protected virtual IList<Deployment> Deployments
        {
            get
            {
                if (_deployments == null)
                    _deployments = LoadData();

                return _deployments;
            }
            set
            {
                _deployments = value;
            }
        }

        #endregion

        #region ctor

        public DeploymentRepository()
            : base()
        {
        }

        #endregion

        #region public

        public async Task<Deployment> InsertAsync(Deployment deployment)
        {
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));

            if (deployment.Version == null)
                throw new ArgumentException(nameof(deployment.Version));

            if (Deployments.Any(d => d.Version == deployment.Version))
                throw new ArgumentException($"Error creating deployment version {deployment.Version}. Version already exists.");

            _deployments.Add(deployment);

            SaveChanges();

            return await Task.FromResult(deployment);
        }

        public async Task DeleteAsync(Version version)
        {
            if (version == null)
                throw new ArgumentException(nameof(version));

            var deployment = await GetAsync(version);

            Deployments.Remove(deployment);

            SaveChanges();
        }

        public async Task<Deployment> GetAsync(Version version)
        {
            if (version == null)
                throw new ArgumentException(nameof(version));

            var deployment = Deployments.FirstOrDefault(d => d.Version == version);

            if (deployment == null)
                throw new ArgumentException($"Version {version} not found");

            return await Task.FromResult(deployment);
        }

        public async Task<IEnumerable<Deployment>> GetListAsync()
        {
            return await Task.FromResult(Deployments.ToList());
        }

        public async Task<IEnumerable<Deployment>> GetRangeAsync(Version startVersion, Version endVersion)
        {
            if (startVersion == null)
                throw new ArgumentException(nameof(startVersion));

            if (endVersion == null)
                throw new ArgumentException(nameof(endVersion));

            return await Task.FromResult(Deployments.Where(d => d.Version >= startVersion && d.Version <= endVersion));
        }

        public async Task<Deployment> UpdateAsync(Deployment deployment)
        {
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));

            if (deployment.Version == null)
                throw new ArgumentException(nameof(deployment.Version));

            var originalDeployment = await GetAsync(deployment.Version);

            Deployments.Remove(originalDeployment);

            originalDeployment.UpdateFrom(deployment);

            return await InsertAsync(originalDeployment);
        }

        #endregion

        #region protected

        protected virtual void SaveChanges()
        {
            Save(Deployments.ToList());
        }

        protected virtual IList<Deployment> LoadData()
        {
            var data = Load();

            if (data == null)
                data = new List<Deployment>();

            return data;
        }

        #endregion
    }
}
