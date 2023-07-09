using AutoMapper;
using NLayer.Core.Dtos;
using NLayer.Core.Dtos.ProductWithCategoryDtos;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWork;

namespace NLayer.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork uow, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, uow)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CategoryWithProducts>> GetSingleCategoryByIdWithProductsAsync(int id)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(id);
            var categoryDto = _mapper.Map<CategoryWithProducts>(category);
            return CustomResponseDto<CategoryWithProducts>.Success(200, categoryDto);
        }
    }
}
