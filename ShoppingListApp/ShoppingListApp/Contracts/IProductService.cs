using ShoppingListApp.Data.Models;
using ShoppingListApp.ViewModels;

namespace ShoppingListApp.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        Task<ProductViewModel> GetByIdAsync(int id);

        Task AddAsync(ProductViewModel viewModel);

        Task UpdateAsync(ProductViewModel viewModel);

        Task DeleteAsync(int id);
    }
}
