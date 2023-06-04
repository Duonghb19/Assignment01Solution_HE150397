using BusinessObject.Models;

namespace Repositories.Categories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        void SaveCategory(Category category);
        Category GetCategoryById(int id);
        void DeleteCategory(Category cateogory);
        void UpdateCategory(Category cateogry);
        List<Product> GetProducts();
    }
}
