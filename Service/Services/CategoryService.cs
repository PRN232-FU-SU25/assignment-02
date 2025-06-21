using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Repository.Models;
using Repository.Models.DTOs.Request;
using Repository.Models.DTOs.Response;
using Repository.Repositories;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }
        public async Task<CategoryResponse> CreateAsync(CategoryRequest dto)
        {
            var cate = _mapper.Map<Category>(dto);
            await _categoryRepo.AddAsync(cate);
            var res = _mapper.Map<CategoryResponse>(cate);
            return res;
        }

        public async Task<(bool Success, string Message)> DeleteAsync(short id)
        {
            var cate = await _categoryRepo.GetCategoryById(id);
            if (cate == null) return (false, "Category not found");

            if (await _categoryRepo.HasNewsArticlesAsync(id))
                return (false, "Cannot delete Category with has articles");

            await _categoryRepo.Delete(cate);
            return (true, "Category deleted");
        }

        public async Task<CategoryResponse?> GetByIdAsync(short id)
        {
            var cate = await _categoryRepo.GetCategoryById(id);
            if (cate == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            var res = _mapper.Map<CategoryResponse?>(cate);
            return res;
        }

        public async Task<List<CategoryResponse>> GetQueryable()
        {
            var accs = await _categoryRepo.GetQueryable();
            var list = await accs.ToListAsync();
            var res = _mapper.Map<List<CategoryResponse>>(list);
            return res;
        }

        public async Task<CategoryResponse> UpdateAsync(short id, CategoryRequest dto)
        {
            var cate = await _categoryRepo.GetCategoryById(id);
            if (cate == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            _mapper.Map(dto, cate);
            cate.ParentCategoryId = dto.ParentCategoryId;
            var parent = await _categoryRepo.GetCategoryById((short)dto.ParentCategoryId);
            cate.ParentCategory = parent;
            await _categoryRepo.UpdateAsync(cate);
            var res = _mapper.Map<CategoryResponse>(cate);
            return res;
        }
    }
}
