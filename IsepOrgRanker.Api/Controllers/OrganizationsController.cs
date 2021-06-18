using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IsepOrgRanker.Api.Model;
using IsepOrgRanker.Api.Services;

namespace IsepOrgRanker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationService _organizationService;

        public OrganizationsController()
        {
            _organizationService = new OrganizationService();
        }

        [HttpGet]
        public IEnumerable<Organization> ListOrganizations()
        {
            return _organizationService.ListOrganizationsByScoreDescending();
        }

        [HttpPost]
        [Route("{name}/votes")]
        public IActionResult VoteForOrganization(string name)
        {
            _organizationService.VoteForOrganization(name);
            

            return NoContent();
        }
    }
}
