using BusinessObject.Models;
using DataAccess;

namespace Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(Order order) => OrderDAO.DeleteOrder(order);

        public Member GetMember(int memberId) => MemberDAO.GetMemberById(memberId);

        public Order GetOrderById(int id) => OrderDAO.GetOrderById(id);

        public List<OrderDetail> GetOrderDetail() => OrderDetailDAO.GetOrderDetail();

        public List<Order> GetOrders() => OrderDAO.GetOrder();

        public void SaveOrder(Order order) => OrderDAO.SaveOrder(order);

        public void UpdateOrder(Order order) => OrderDAO.UpdateOrder(order);
    }
}
