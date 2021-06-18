using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IsepOrgRanker.Business.Model;
using IsepOrgRanker.Business.Services;

namespace IsepOrgRanker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationService _organizationService;

        public OrganizationsController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
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
