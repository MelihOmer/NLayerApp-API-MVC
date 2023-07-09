using NLayer.Core.Dtos.CategoryDtos;
using NLayer.Core.Dtos.ProductDtos;

namespace NLayer.Core.Dtos.ProductWithCategoryDtos
{
    public class CategoryWithProducts : CategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
