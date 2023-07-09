namespace NLayer.Core.Dtos.ProductDtos
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
        public int CategoryId { get; set; }
    }
}
