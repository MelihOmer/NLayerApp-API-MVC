using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts([FromRoute] int id)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductsAsync(id));
        }

    }
}
