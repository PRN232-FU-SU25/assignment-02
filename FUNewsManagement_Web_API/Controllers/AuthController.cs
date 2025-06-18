using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.DTOs.Response;
using Repository.Models.DTOs.Resquest;
using Services.IService;

namespace FUNewsManagement_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISystemAccountService _service;
        public AuthController(ISystemAccountService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
                var token = await _service.LoginAsync(request);
                return Ok(token);
        } 
    }
}
