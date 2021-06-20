using System;
using System.Collections.Generic;
using System.Text;
using IsepOrgRanker.Business.Model;

namespace IsepOrgRanker.Persistence.InMemory.Entities
{
    public class OrganizationEntity
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public Organization ToBusinessModel()
        {
            return new Organization()
            {
                Name = this.Name,
                Score = this.Score
            };
        }

        public static OrganizationEntity FromBusinessModel(Organization organization)
        {
            return new OrganizationEntity()
            {
                Name = organization.Name,
                Score = organization.Score
            };
        }
    }
}
