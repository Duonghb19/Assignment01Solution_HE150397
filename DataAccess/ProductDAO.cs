using BusinessObject.Models;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    listProducts = connection.Products.ToList();
                    foreach (var item in listProducts)
                    {
                        item.Category = CategoryDAO.GetCategoryById(item.CategoryId);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listProducts;
        }
        public static List<Product> GetAllProductsByCategoryId(int id)
        {
            var listProducts = new List<Product>();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    listProducts = connection.Products.Where(x => x.CategoryId == id).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listProducts;
        }
        public static Product FindProductById(int prodId)
        {
            Product p = new Product();
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    p = context.Products.SingleOrDefault(x => x.ProductId == prodId);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return p;
        }
        public static void SaveProduct(Product p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void UpdateProduct(Product p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var p1 = context.Products.SingleOrDefault(c => c.ProductId == p.ProductId);
                    context.Products.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
