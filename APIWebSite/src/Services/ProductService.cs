using APIWebSite.src.Repository;
using WebSiteClassLibrary.Models;

namespace APIWebSite.src.Services
{
    public class ProductService : WebSiteClassLibrary.Interfaces.Services.IProductService
    {
        private readonly WebSiteClassLibrary.Interfaces.Repository.IProductRepository _productRepository;

        public ProductService(WebSiteClassLibrary.Interfaces.Repository.IProductRepository _productRepository)
        {
            this._productRepository = _productRepository;
        }

        public async Task AddProductAsync(WebSiteClassLibrary.DTO.ProductDTO product)
        {
            if (await ((BaseRepository<Product>)_productRepository).ExistAsync(product.Productname))
                throw new InvalidOperationException("The product already exists");
            var newProduct = new Product
            {
                Productname = product.Productname,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                ShopId = product.ShopId,
                SubcategoryID = product.SubcategoryID,
            };
            await ((BaseRepository<Product>)_productRepository).AddAsync(newProduct);
        }
        public async Task<IEnumerable<Product>?> GetAllProductsAsync()
        {
            return await ((BaseRepository<Product>)_productRepository).GetAllAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            await ((BaseRepository<Product>)_productRepository).DeleteAsync(id);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await ((BaseRepository<Product>)_productRepository).GetByIDAsync(id);
        }
        public async Task UpdateProductAsync(WebSiteClassLibrary.DTO.ProductDTO dto)
        {
            var product = await ((BaseRepository<Product>)_productRepository).GetByIDAsync(dto.id);
            if(product != null)
                await ((BaseRepository<Product>)_productRepository).UpdateAsync(product);
        }
        public async Task<List<Product>?> GetByCategoryAsync(string category)
        {
            return await _productRepository.GetProductsByCategoryAsync(category);
        }
    }
}
