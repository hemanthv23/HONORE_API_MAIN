using HONORE_API_MAIN.Data;
using HONORE_API_MAIN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection; // For PropertyInfo
using System; // For Convert.ChangeType

namespace HONORE_API_MAIN.Services
{
    public class ProductService : IProductService
    {
        private readonly HonoreDBContext _context;

        public ProductService(HonoreDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            return await _context.Products
                                 .Where(p => p.Category.ToLower() == categoryName.ToLower())
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBreadSubcategoryAsync(string subCategoryName)
        {
            return await _context.Products
                                 .Where(p => p.BreadSubcategory != null &&
                                             p.BreadSubcategory.ToLower() == subCategoryName.ToLower())
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            string lowerSearchTerm = searchTerm.ToLower();

            return await _context.Products
                                 .Where(p =>
                                     p.Name.ToLower().StartsWith(lowerSearchTerm) ||
                                     p.Name.ToLower().Contains(lowerSearchTerm) ||
                                     p.Description.ToLower().Contains(lowerSearchTerm) ||
                                     p.Category.ToLower().Contains(lowerSearchTerm) ||
                                     (p.BreadSubcategory != null && p.BreadSubcategory.ToLower().Contains(lowerSearchTerm))
                                 )
                                 .ToListAsync();
        }

        public async Task<bool> UpdateProductFieldAsync(int id, string fieldName, object value)
        {
            var productToUpdate = await _context.Products.FindAsync(id);
            if (productToUpdate == null)
            {
                return false;
            }

            PropertyInfo? property = typeof(Product).GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (property == null || !property.CanWrite)
            {
                return false;
            }

            try
            {
                object convertedValue = Convert.ChangeType(value, property.PropertyType);
                property.SetValue(productToUpdate, convertedValue);
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var productToRemove = await _context.Products.FindAsync(id);
            if (productToRemove == null)
            {
                return false;
            }

            _context.Products.Remove(productToRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _context.Products
                                 .Select(p => p.Category)
                                 .Where(c => !string.IsNullOrEmpty(c))
                                 .Distinct()
                                 .OrderBy(c => c)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllBreadSubcategoriesAsync()
        {
            return await _context.Products
                                 .Select(p => p.BreadSubcategory)
                                 .Where(sc => !string.IsNullOrEmpty(sc))
                                 .Distinct()
                                 .OrderBy(sc => sc)
                                 .ToListAsync()!;
        }
    }
}