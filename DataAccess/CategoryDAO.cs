using BusinessObject.Models;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCate = new List<Category>();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    listCate = connection.Categories.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listCate;
        }
        public static Category GetCategoryById(int id)
        {
            var category = new Category();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    category = connection.Categories.SingleOrDefault(x => x.CategoryId == id);
                    category.Products = ProductDAO.GetAllProductsByCategoryId(category.CategoryId);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return category;
        }
        public static void SaveCategory(Category cate)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Categories.Add(cate);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void UpdateCategory(Category cate)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Entry<Category>(cate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void DeleteCategory(Category cate)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var p1 = context.Categories.SingleOrDefault(c => c.CategoryId == cate.CategoryId);
                    context.Categories.Remove(p1);
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
