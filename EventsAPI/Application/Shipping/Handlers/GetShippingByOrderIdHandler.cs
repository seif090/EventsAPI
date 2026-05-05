using EventsAPI.Application.Shipping.Models;
using EventsAPI.Application.Shipping.Queries;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Shipping.Handlers;

public class GetShippingByOrderIdHandler : IRequestHandler<GetShippingByOrderIdQuery, ShippingDto?>
{
    private readonly AppDbContext _dbContext;

    public GetShippingByOrderIdHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ShippingDto?> Handle(GetShippingByOrderIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Shippings
            .Where(shipping => shipping.OrderId == request.OrderId)
            .Select(shipping => new ShippingDto
            {
                Id = shipping.Id,
                OrderId = shipping.OrderId,
                ProviderName = shipping.ProviderName,
                TrackingNumber = shipping.TrackingNumber,
                Cost = shipping.Cost,
                Status = shipping.Status,
                AddressLine1 = shipping.AddressLine1,
                AddressLine2 = shipping.AddressLine2,
                City = shipping.City,
                Governorate = shipping.Governorate,
                PostalCode = shipping.PostalCode
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
