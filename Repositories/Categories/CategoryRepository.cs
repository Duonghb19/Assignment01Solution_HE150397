using BusinessObject.Models;
using DataAccess;

namespace Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void DeleteCategory(Category cateogory) => CategoryDAO.DeleteCategory(cateogory);

        public List<Category> GetCategories() => CategoryDAO.GetCategories();

        public Category GetCategoryById(int id) => CategoryDAO.GetCategoryById(id);

        public List<Product> GetProducts() => ProductDAO.GetProducts();

        public void SaveCategory(Category category) => CategoryDAO.SaveCategory(category);

        public void UpdateCategory(Category cateogry) => CategoryDAO.UpdateCategory(cateogry);
    }
}
