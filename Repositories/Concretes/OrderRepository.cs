
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using StoreApp.Repositories;

namespace Repositories.Concretes
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Order> Orders => 
            _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(cl => cl.Product)
            .OrderBy(o => o.isShipped)
            .ThenByDescending(o => o.OrderId);

        public int NumberOfInProcess => _context.Orders.Count(o => o.isShipped.Equals(false));

        public void Complete(int id)
        {
            var order = FindByCondition(o => o.OrderId.Equals(id),true);
            if (order is null)
                throw new Exception("Order could not found");
            
            order.isShipped = true;
        }

        public Order? GetOneOrder(int id)
        {
            return FindByCondition(o => o.OrderId.Equals(id), false);
        }

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
                _context.Orders.Add(order);

            _context.SaveChanges() ;
        }
    }
}
