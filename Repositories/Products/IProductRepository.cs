using BusinessObject.Models;

namespace Repositories.Products
{
    public interface IProductRepository
    {
        void SaveProduct(Product p);
        Product GetProductById(int id);
        void DeleteProduct(Product p);
        void UpdateProduct(Product p);
        List<Category> GetCategories();
        List<Product> GetProducts();
    }
}
