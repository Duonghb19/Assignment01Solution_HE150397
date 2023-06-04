using BusinessObject.Models;

namespace DataAccess
{
    public class OrderDAO
    {
        public static List<Order> GetOrder()
        {
            var listOrder = new List<Order>();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    listOrder = connection.Orders.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOrder;
        }
        public static Order GetOrderById(int id)
        {
            var order = new Order();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    order = connection.Orders.SingleOrDefault(x => x.OrderId == id);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return order;
        }
        public static void SaveOrder(Order order)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void UpdateOrder(Order order)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void DeleteOrder(Order ord)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var order = context.Orders.SingleOrDefault(c => c.OrderId == ord.OrderId);
                    context.Orders.Remove(order);
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
