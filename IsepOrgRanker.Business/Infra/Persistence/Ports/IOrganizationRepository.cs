using System;
using System.Collections.Generic;
using System.Text;
using IsepOrgRanker.Business.Model;

namespace IsepOrgRanker.Business.Infra.Persistence.Ports
{
    public interface IOrganizationRepository
    {
        IEnumerable<Organization> ListAllOrganizationsByScoreDescending();

        Organization FindOrganizationByName(string name);

        void SaveOrganization(Organization organizationToSave);
    }
}
