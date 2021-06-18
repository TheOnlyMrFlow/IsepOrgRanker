using System.Collections.Generic;
using IsepOrgRanker.Business.Infra.Persistence.Ports;
using IsepOrgRanker.Business.Model;

namespace IsepOrgRanker.Business.Services
{
    public class OrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        public IEnumerable<Organization> ListOrganizationsByScoreDescending()
        {
            return _organizationRepository.ListAllOrganizationsByScoreDescending();
        }

        public void VoteForOrganization(string name)
        {
            var organizationToVoteFor = _organizationRepository.FindOrganizationByName(name);

            organizationToVoteFor.Score++;

            _organizationRepository.SaveOrganization(organizationToVoteFor);
        }
    }
}
