using System.Collections.Generic;
using System.Linq;
using IsepOrgRanker.Business.Infra.Persistence.Ports;
using IsepOrgRanker.Business.Model;
using IsepOrgRanker.Persistence.InMemory.Entities;

namespace IsepOrgRanker.Persistence.InMemory.Repositories
{
    public class OrganizationInMemoryRepository : IOrganizationRepository
    {
        private readonly List<OrganizationEntity> _organizationEntities = new List<OrganizationEntity>()
        {
            new OrganizationEntity() { Name = "IsepBand", Score = 0},
            new OrganizationEntity() { Name = "IsepLive", Score = 0},
            new OrganizationEntity() { Name = "Dionysos", Score = 0}
        };

        public IEnumerable<Organization> ListAllOrganizationsByScoreDescending()
        {
            return
                _organizationEntities
                    .OrderByDescending(entity => entity.Score)
                    .Select(entity => new Organization() 
                    {
                        Name = entity.Name,
                        Score = entity.Score
                    });
        }

        public Organization FindOrganizationByName(string name)
        {
            return _organizationEntities
                .FirstOrDefault(org => org.Name == name)?
                .ToBusinessModel();
        }

        public void SaveOrganization(Organization organizationToSave)
        {
            _organizationEntities.RemoveAll(e => e.Name == organizationToSave.Name);

            var newEntity = OrganizationEntity.FromBusinessModel(organizationToSave);
            _organizationEntities.Add(newEntity);
        }
    }
}
