using NLayer.Core.Dtos.CategoryDtos;
using NLayer.Core.Dtos.ProductDtos;

namespace NLayer.Core.Dtos.ProductWithCategoryDtos
{
    public class ProductWithCategoryDto : ProductDto
    {
        public CategoryDto Category { get; set; }
    }
}
