using EventsAPI.Application.Orders.Models;
using EventsAPI.Application.Orders.Queries;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Orders.Handlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly AppDbContext _dbContext;

    public GetOrderByIdHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Where(order => order.Id == request.OrderId)
            .Select(order => new OrderDto
            {
                Id = order.Id,
                BookingId = order.BookingId,
                ClientId = order.ClientId,
                AlbumTypeId = order.AlbumTypeId,
                BoxTypeId = order.BoxTypeId,
                Status = order.Status,
                TotalPrice = order.TotalPrice
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
