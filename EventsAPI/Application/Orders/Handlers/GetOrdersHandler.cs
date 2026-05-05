using EventsAPI.Application.Orders.Models;
using EventsAPI.Application.Orders.Queries;
using EventsAPI.Infrastructure.Persistence;
using EventsAPI.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Orders.Handlers;

public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, PagedResult<OrderDto>>
{
    private readonly AppDbContext _dbContext;

    public GetOrdersHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Orders.AsQueryable();

        if (request.ClientId.HasValue)
        {
            query = query.Where(order => order.ClientId == request.ClientId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(order => order.Status == request.Status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderByDescending(order => order.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
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
            .ToListAsync(cancellationToken);

        return new PagedResult<OrderDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}
