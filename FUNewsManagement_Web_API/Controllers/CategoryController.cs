using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.Models.DTOs.Request;
using Repository.Models.DTOs.Response;
using Services.IService;
using System.ComponentModel.DataAnnotations;

namespace FUNewsManagement_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service) {
        _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<TResponse<List<CategoryResponse>>>> GetAll()
        {
            var categories = await _service.GetQueryable();
            var res = new TResponse<List<CategoryResponse>>("get list category success", categories);
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<TResponse<CategoryResponse>>> CreateCategory([FromBody] CategoryRequest req)
        {
            var cate = await _service.CreateAsync(req);
            var res = new TResponse<CategoryResponse>("category created",cate);
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TResponse<CategoryResponse>>> UpdateCategory(short id, [FromBody] CategoryRequest req)
        {
            var cate = await _service.UpdateAsync(id,req);
            var res = new TResponse<CategoryResponse>("category updated", cate);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(short id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TResponse<CategoryResponse>>> GetById(short id)
        {
            {
                var cate = await _service.GetByIdAsync(id);
                var res = new TResponse<CategoryResponse>("get category success ", cate);
                return Ok(res);
            }
        }

    }
}
