using EventsAPI.Application.Shipping.Commands;
using EventsAPI.Application.Shipping.Models;
using EventsAPI.Domain.Entities;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Shipping.Handlers;

public class CreateShippingHandler : IRequestHandler<CreateShippingCommand, ShippingDto>
{
    private readonly AppDbContext _dbContext;

    public CreateShippingHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ShippingDto> Handle(CreateShippingCommand request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        if (order is null)
        {
            throw new InvalidOperationException("InvalidOrder");
        }

        var exists = await _dbContext.Shippings.AnyAsync(s => s.OrderId == request.OrderId, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("ShippingAlreadyExists");
        }

        var shipping = new Domain.Entities.Shipping
        {
            OrderId = request.OrderId,
            ProviderName = request.ProviderName,
            Cost = request.Cost,
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            City = request.City,
            Governorate = request.Governorate,
            PostalCode = request.PostalCode
        };

        _dbContext.Shippings.Add(shipping);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ShippingDto
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
        };
    }
}
