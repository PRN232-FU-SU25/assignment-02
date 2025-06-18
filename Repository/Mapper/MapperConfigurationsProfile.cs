using AutoMapper;
using Repository.Models;
using Repository.Models.DTOs.Request;
using Repository.Models.DTOs.Response;
using Repository.Models.DTOs.Resquest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Mapper
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<SystemAccount, AccountResponse>();
            CreateMap<AccountRequest, SystemAccount>()
                .ForMember(dest => dest.AccountId, opt => opt.Ignore());
            CreateMap<Category, CategoryResponse>()
            .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.CategoryName : null));
            CreateMap<CategoryRequest, Category>();
            CreateMap<NewsArticle, NewsArticleResponse>()
             .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.AccountName))
             .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName));
            CreateMap<NewsArticleRequest, NewsArticle>();
        }
    }
}
