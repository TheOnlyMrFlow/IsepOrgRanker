using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IsepOrgRanker.Business.Infra.Persistence.Ports;
using IsepOrgRanker.Business.Model;

namespace IsepOrgRanker.Persistence.Csv.Repositories
{
    public class OrganizationCsvRepository : IOrganizationRepository
    {
        private readonly string _organizationsStorageFilePath;

        public OrganizationCsvRepository()
        {
            _organizationsStorageFilePath = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "IsepOrgRanker",
                "orgs.csv");
        }

        public IEnumerable<Organization> ListAllOrganizationsByScoreDescending()
        {
            return
                ListAllOrganizations()
                .OrderByDescending(org => org.Score);
        }

        public Organization FindOrganizationByName(string name)
        {
            return ListAllOrganizations()
                .First(org => org.Name == name);
        }

        public void SaveOrganization(Organization organizationToSave)
        {
            var updatedOrganizationsList =
                ListAllOrganizations()
                    .Where(org => org.Name != organizationToSave.Name)
                    .Append(organizationToSave)
                    .ToList();

            var allOrganizationLines =
                updatedOrganizationsList
                    .Select(org => string.Join(',', org.Name, org.Score.ToString()));
            
            File.WriteAllLines(_organizationsStorageFilePath, allOrganizationLines);
        }

        private IEnumerable<Organization> ListAllOrganizations()
        {
            return File.ReadLines(_organizationsStorageFilePath)
                .Select(line => line.Split(','))
                .Select(splitLine => new Organization
                {
                    Name = splitLine[0],
                    Score = int.Parse(splitLine[1])
                });
        }
    }
}
