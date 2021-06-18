using System.Collections.Generic;
using IsepOrgRanker.Api.Model;
using IsepOrgRanker.Api.Repositories;

namespace IsepOrgRanker.Api.Services
{
    public class OrganizationService
    {
        private readonly OrganizationRepository _organizationReposiory;

        public OrganizationService()
        {
            _organizationReposiory = new OrganizationRepository();
        }
        public IEnumerable<Organization> ListOrganizationsByScoreDescending()
        {
            return _organizationReposiory.ListAllOrganizationsByScoreDescending();
        }

        public void VoteForOrganization(string name)
        {
            var organizationToVoteFor = _organizationReposiory.FindOrganizationByName(name);

            organizationToVoteFor.Score++;

            _organizationReposiory.SaveOrganization(organizationToVoteFor);
        }
    }
}
