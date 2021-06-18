using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IsepOrgRanker.Api.Model;

namespace IsepOrgRanker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly string _organizationsStorageFilePath;

        public OrganizationsController()
        {
            _organizationsStorageFilePath = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "IsepOrgRanker",
                "orgs.csv");
        }

        [HttpGet]
        public IEnumerable<Organization> ListOrganizations()
        {
            return System.IO.File.ReadLines(_organizationsStorageFilePath)
                .Select(line => line.Split(','))
                .Select(splitLine => new Organization
                {
                    Name = splitLine[0],
                    Score = int.Parse(splitLine[1])
                })
                .OrderByDescending(org => org.Score);
        }

        [HttpPost]
        [Route("{name}/votes")]
        public IActionResult VoteForOrganization(string name)
        {
            var organizations = System.IO.File.ReadLines(_organizationsStorageFilePath)
                .Select(line => line.Split(','))
                .Select(splitLine => new Organization
                {
                    Name = splitLine[0],
                    Score = int.Parse(splitLine[1])
                }).ToList();

            organizations.First(org => org.Name == name).Score += 1;

            System.IO.File.WriteAllLines
                (_organizationsStorageFilePath,
                organizations.Select(org => string.Join(',', org.Name, org.Score.ToString())));

            return NoContent();
        }
    }
}
