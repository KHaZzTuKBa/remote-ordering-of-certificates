using Application.Contracts;
using Application.DTOs.User.CreateRequest;
using Application.DTOs.User.GetRequest;
using Application.DTOs.User.GetRequestList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : Controller
    {
        private readonly IUser user;
        private readonly IConfiguration configuration;

        public User(IUser user, IConfiguration configuration)
        {
            this.user = user;
            this.configuration = configuration;
        }

        [HttpPost("CreateRequest")]
        public async Task<ActionResult<CreateRequestResponse>> CreateRequest(CreateRequestDTO createRequestDTO)
        {
            var result = user.CreateRequest (createRequestDTO);

            return Ok(result);
        }

        [HttpPost("GetRequest")]
        public async Task<ActionResult<GetRequestResponse>> GetRequest(GetRequestDTO getRequestDTO)
        {
            var result = user.GetRequest(getRequestDTO);

            return Ok(result);
        }

        [HttpPost("GetRequestList")]
        public async Task<ActionResult<GetRequestListResponse>> GetRequestList(GetRequestListDTO getRequestListDTO)
        {
            var result = user.GetRequestList(getRequestListDTO);

            return Ok(result);
        }
    }
}
