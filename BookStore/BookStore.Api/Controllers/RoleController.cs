using Microsoft.AspNetCore.Mvc;
using BookStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("role")]
    public class RoleController : ControllerBase
    {
        RoleRepository _repository = new RoleRepository();
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(List<RoleModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllRoles()
        {
            var roles = _repository.getAllRoles();
            ListResponse<RoleModel> listResponse = new ListResponse<RoleModel>()
            {
                Results = roles.Results.Select(c => new RoleModel(c)),
                TotalRecords = roles.TotalRecords,
            };

            return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
        }
    }
}
