using BusinessObject.Models;

namespace Repositories.OrderDetails
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail od);
        OrderDetail GetOrderDetailById(int id);
        Order GetOrderByOrderId(int odID);
        Product GetProductbyProductID(int odID);
        List<OrderDetail> GetOrderDetails();
    }
}
