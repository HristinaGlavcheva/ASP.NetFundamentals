using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Contracts;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.ViewModels;

namespace ShoppingListApp.Services
{
    public class ProductService : IProductService
    {
        private readonly ShoppingListDbContext dbContext;

        public ProductService(ShoppingListDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task AddAsync(ProductViewModel viewModel)
        {
            var product = new Product
            {
                Name = viewModel.Name
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException("Invalid product");
            }

            dbContext.Remove(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await dbContext.Products
                .AsNoTracking()
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToListAsync();

            return products;
        }

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if(product == null)
            {
                throw new ArgumentException("Invalid product");
            }

            return new ProductViewModel
            {
                Id = id,
                Name = product.Name 
            };
        }

        public async Task UpdateAsync(ProductViewModel viewModel)
        {
            var product = await dbContext.Products.FindAsync(viewModel.Id);

            if (product == null)
            {
                throw new ArgumentException("Invalid product");
            }

            product.Name = viewModel.Name;

            await dbContext.SaveChangesAsync();
        }
    }
}
