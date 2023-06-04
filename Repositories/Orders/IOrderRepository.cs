using BusinessObject.Models;

namespace Repositories.Orders
{
    public interface IOrderRepository
    {
        void SaveOrder(Order order);
        Order GetOrderById(int id);
        void DeleteOrder(Order order);
        void UpdateOrder(Order order);
        Member GetMember(int memberId);
        List<OrderDetail> GetOrderDetail();
        List<Order> GetOrders();
    }
}
