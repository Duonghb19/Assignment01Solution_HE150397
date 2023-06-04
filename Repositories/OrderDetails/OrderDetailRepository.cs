using BusinessObject.Models;
using DataAccess;

namespace Repositories.OrderDetails
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        public Order GetOrderByOrderId(int odID) => OrderDAO.GetOrderById(odID);

        public OrderDetail GetOrderDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetail();

        public Product GetProductbyProductID(int odID) => ProductDAO.FindProductById(odID);

        public void SaveOrderDetail(OrderDetail od) => OrderDetailDAO.SaveOrderDetail(od);

    }
}
