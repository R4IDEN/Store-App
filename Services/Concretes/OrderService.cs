
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;

namespace Services.Concretes
{
    public class OrderService : IOrderService
    {   
        private readonly IRepositoryManager _repositoryManager;

        public OrderService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public IQueryable<Order> Orders => _repositoryManager.Order.Orders;

        public int NumberOfInProcess => _repositoryManager.Order.NumberOfInProcess;

        public void Complete(int id)
        {
            _repositoryManager.Order.Complete(id);
            _repositoryManager.Save();
        }

        public Order? GetOneOrder(int id)
        {
            return _repositoryManager.Order.GetOneOrder(id);
        }

        public void SaveOrder(Order order)
        {
            _repositoryManager.Order.SaveOrder(order);
        }
    }
}
