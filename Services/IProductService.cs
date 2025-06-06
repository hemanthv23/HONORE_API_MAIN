using HONORE_API_MAIN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HONORE_API_MAIN.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName);
        Task<IEnumerable<Product>> GetProductsByBreadSubcategoryAsync(string subCategoryName);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<bool> UpdateProductFieldAsync(int id, string fieldName, object value);
        Task<bool> DeleteProductAsync(int id);

        Task<IEnumerable<string>> GetAllCategoriesAsync();
        Task<IEnumerable<string>> GetAllBreadSubcategoriesAsync();
    }
}