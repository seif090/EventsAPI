using EventsAPI.Application.Shipping.Commands;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Shipping.Handlers;

public class UpdateShippingStatusHandler : IRequestHandler<UpdateShippingStatusCommand, bool>
{
    private readonly AppDbContext _dbContext;

    public UpdateShippingStatusHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateShippingStatusCommand request, CancellationToken cancellationToken)
    {
        var shipping = await _dbContext.Shippings.FirstOrDefaultAsync(s => s.Id == request.ShippingId, cancellationToken);
        if (shipping is null)
        {
            return false;
        }

        shipping.Status = request.Status;
        shipping.TrackingNumber = request.TrackingNumber ?? shipping.TrackingNumber;
        shipping.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
