using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Dtos;
using NLayer.Core.Dtos.ProductWithCategoryDtos;
using NLayer.Core.Entities;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWork;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Caching
{
    public class ProductServiceCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceCaching(IMapper mapper, IMemoryCache cache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _cache = cache;
            _repository = repository;
            _unitOfWork = unitOfWork;
            if (!_cache.TryGetValue(CacheProductKey, out _))
            {
                _cache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_cache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _cache.Get<List<Product>>(CacheProductKey).FirstOrDefault(p => p.Id == id);
            if (product == null)
                throw new NotFoundException($"{typeof(Product).Name} ({id}) not found.");

            return Task.FromResult(product);

        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = _cache.Get<IEnumerable<Product>>(CacheProductKey);
            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto));
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> filter)
        {
            return _cache.Get<List<Product>>(CacheProductKey).Where(filter.Compile()).AsQueryable();
        }
        private async Task CacheAllProductsAsync()
        {
            _cache.Set(CacheProductKey, await _repository.GetProductsWithCategory());
        }
    }
}
