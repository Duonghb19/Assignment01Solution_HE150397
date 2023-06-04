using BusinessObject.Models;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetail()
        {
            var listOrderDetail = new List<OrderDetail>();
            try
            {
                using (var connection = new PRN231_AS1Context())
                {
                    listOrderDetail = connection.OrderDetails.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOrderDetail;
        }
        //public static OrderDetail GetOrderDetailById(int id)
        //{
        //    var orderDetail = new OrderDetail();
        //    try
        //    {
        //        using (var connection = new PRN231_AS1Context())
        //        {
        //            orderDetail = connection.OrderDetails.SingleOrDefault(x => x.O == id);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return member;
        //}
        public static void SaveOrderDetail(OrderDetail od)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.OrderDetails.Add(od);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static void UpdateOrderDetail(OrderDetail od)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    context.Entry<OrderDetail>(od).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
