using Clean_arch.Application.Orders.DTOs;
using Clean_arch.Domain.Orders;
using Clean_arch.Domain.Orders.Repository;

namespace Clean_arch.Application.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public void AddOrder(AddOrderDto command)
    {
        var order = new Order(command.ProductId, command.Count, command.Price);
        _repository.Add(order);
        _repository.SaveChanges();
    }

    public void FinallyOrder(FinallyOrderDto command)
    {
        var order = _repository.GetById(command.OrderId);
        order.Finally();
        _repository.Update(order);
        _repository.SaveChanges();
    }

    public OrderDto GetOrderById(long id)
    {
        var order = _repository.GetById(id);
        return new OrderDto()
        {
            Count = order.Count,
            Price = order.Price,
            Id = order.Id,
            ProductId = order.ProductId
        };
    }

    public List<OrderDto> GetOrders()
    {
        return _repository.GetList().Select(order => new OrderDto()
        {
            Count = order.Count,
            Price = order.Price,
            Id = order.Id,
            ProductId = order.ProductId
        }).ToList();
    }
}