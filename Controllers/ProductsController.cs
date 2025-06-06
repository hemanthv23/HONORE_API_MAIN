using HONORE_API_MAIN.Models;
using HONORE_API_MAIN.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http; // Required for StatusCodes

namespace HONORE_API_MAIN.Controllers
{
    [ApiController]
    [Route("api/products")] // Base route for the resource
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET /api/products
        // Gets all products.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        // GET /api/products/{id}
        // Gets a single product by its ID.
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        // GET /api/products/category/{categoryName}
        // Gets products by a specific category.
        [HttpGet("category/{categoryName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryName);

            if (products == null || !products.Any())
            {
                return NotFound($"No products found in category: {categoryName}");
            }
            return Ok(products);
        }

        // IMPORTANT UPDATE HERE:
        // GET /api/products/bread/{subCategoryName}
        // Gets products by a specific bread subcategory.
        [HttpGet("bread/{subCategoryName}")] // CHANGED THIS LINE
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByBreadSubcategory(string subCategoryName)
        {
            var products = await _productService.GetProductsByBreadSubcategoryAsync(subCategoryName);

            if (products == null || !products.Any())
            {
                return NotFound($"No products found in bread subcategory: {subCategoryName}");
            }
            return Ok(products);
        }

        // GET /api/products/search?term=garlic
        // Searches products by a query term.
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var products = await _productService.SearchProductsAsync(term);

            if (products == null || !products.Any())
            {
                return NotFound($"No products found matching '{term}'.");
            }
            return Ok(products);
        }

        // POST /api/products
        // Creates a new product.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.Id = 0; // Ensure client-provided ID doesn't interfere

            var addedProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.Id }, addedProduct);
        }

        // PATCH /api/products/{id}/{fieldName}
        // This endpoint allows updating a single field of a product (partial update).
        [HttpPatch("{id}/{fieldName}")] // Changed to HttpPatch
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductField(int id, string fieldName, [FromBody] object value)
        {
            if (value == null)
            {
                return BadRequest("Value cannot be null.");
            }

            bool updated = await _productService.UpdateProductFieldAsync(id, fieldName, value);

            if (!updated)
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }
                else
                {
                    return BadRequest($"Field '{fieldName}' cannot be updated or does not exist for product ID {id}.");
                }
            }

            return NoContent();
        }

        // DELETE /api/products/{id}
        // Deletes a product by its ID.
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return NoContent();
        }

        // GET /api/products/categories
        // Gets all unique product categories.
        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllCategories()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }

        // GET /api/products/bread-subcategoriesssssssssssssssssssssss
        // Gets all unique bread subcategories.
        [HttpGet("bread-subcategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllBreadSubcategories()
        {
            var subcategories = await _productService.GetAllBreadSubcategoriesAsync();
            if (subcategories == null || !subcategories.Any())
            {
                return NotFound("No bread subcategories found.");
            }
            return Ok(subcategories);
        }
    }
}