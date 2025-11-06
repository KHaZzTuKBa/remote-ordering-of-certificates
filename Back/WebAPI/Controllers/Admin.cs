using Application.Contracts;
using Application.DTOs.Admin.AdminGetRequest;
using Application.DTOs.Admin.AdminGetRequestList;
using Application.DTOs.Admin.SubmitTo1c;
using Application.DTOs.Admin.UpdateFrom1c;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class Admin : Controller
    {
        private readonly IAdmin admin;
        private readonly IConfiguration configuration;

        public Admin(IAdmin admin, IConfiguration configuration)
        {
            this.admin = admin;
            this.configuration = configuration;
        }

        [HttpGet("GetRequest")]
        public async Task<ActionResult<AdminGetRequestResponse>> AdminGetRequest(AdminGetRequestDTO adminGetRequestDTO)
        {
            var result = await admin.AdminGetRequest(adminGetRequestDTO);

            return Ok(result);
        }

        [HttpGet("GetRequestList")]
        public async Task<ActionResult<AdminGetRequestListResponse>> AdminGetRequestList(AdminGetRequestListDTO adminGetRequestListDTO)
        {
            var result = await admin.AdminGetRequestList(adminGetRequestListDTO);
            return Ok(result);
        }

        [HttpPost("SubmitTo1C")]
        public async Task<ActionResult<SubmitTo1cResponse>> SubmitTo1C(SubmitTo1cDTO submitTo1c)
        {
            var result = await admin.SubmitTo1C(submitTo1c);
            return Ok(result);
        }

        [HttpPost("UpdateFrom1C")]
        public async Task<ActionResult<UpdateFrom1cResponse>> UpdateFrom1C(UpdateFrom1cDTO updateFrom1сDTO)
        {
            var result = await admin.UpdateFrom1C(updateFrom1сDTO);

            return Ok(result);
        }
    }
}
