using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IsepOrgRanker.Api.Model;

namespace IsepOrgRanker.Api.Services
{
    public class OrganizationService
    {
        private readonly string _organizationsStorageFilePath;

        public OrganizationService()
        {
            _organizationsStorageFilePath = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "IsepOrgRanker",
                "orgs.csv");
        }
        public IEnumerable<Organization> ListOrganizationsByScoreDescending()
        {
            return File.ReadLines(_organizationsStorageFilePath)
                .Select(line => line.Split(','))
                .Select(splitLine => new Organization
                {
                    Name = splitLine[0],
                    Score = int.Parse(splitLine[1])
                })
                .OrderByDescending(org => org.Score);
        }

        public void VoteForOrganization(string name)
        {
            var organizations = File.ReadLines(_organizationsStorageFilePath)
                .Select(line => line.Split(','))
                .Select(splitLine => new Organization
                {
                    Name = splitLine[0],
                    Score = int.Parse(splitLine[1])
                }).ToList();

            organizations.First(org => org.Name == name).Score += 1;

            File.WriteAllLines
            (_organizationsStorageFilePath,
                organizations.Select(org => string.Join(',', org.Name, org.Score.ToString())));
        }
    }
}
