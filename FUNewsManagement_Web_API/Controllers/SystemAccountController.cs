using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Models.DTOs.Response;
using Repository.Models.DTOs.Resquest;
using Services.IService;

namespace FUNewsManagement_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAccountController : ControllerBase
    {
        private readonly ISystemAccountService _service;
        public SystemAccountController(ISystemAccountService service)
        {
            _service = service; 
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<TResponse<List<AccountResponse>>>> GetAll()
        {
            var accs = await _service.GetQueryable();
            var res = new TResponse<List<AccountResponse>>("lấy danh sách tài khoản thành công", accs);
            return Ok(res);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TResponse<AccountResponse>>> GetById(short id)
        {
            var acc = await _service.GetByIdAsync(id);
            var res = new TResponse<AccountResponse>("lấy thông tin tài khoản thành công", acc);
            return Ok(res);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<TResponse<AccountResponse>>> CreateAccount([FromBody] AccountRequest req)
        {
            var acc = await _service.CreateAsync(req);
            var res = new TResponse<AccountResponse>("tạo tài khoản thành công", acc);
             return Ok(res);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAccount(short id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<TResponse<AccountResponse>>> UpdateAccount(short id, [FromBody] AccountRequest reqs)
        {
            var acc = await _service.UpdateAsync(id, reqs);
            var res = new TResponse<AccountResponse>("cập nhật tài khoản thành công", acc);
            return Ok(res);
        }
    }
}

