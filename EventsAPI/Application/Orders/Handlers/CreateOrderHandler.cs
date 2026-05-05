using EventsAPI.Application.Orders.Commands;
using EventsAPI.Application.Orders.Models;
using EventsAPI.Domain.Entities;
using EventsAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Orders.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly AppDbContext _dbContext;

    public CreateOrderHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var booking = await _dbContext.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken);
        if (booking is null || booking.ClientId != request.ClientId)
        {
            throw new InvalidOperationException("InvalidBooking");
        }

        var album = await _dbContext.AlbumTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == request.AlbumTypeId && a.IsActive, cancellationToken);
        if (album is null)
        {
            throw new InvalidOperationException("InvalidAlbumType");
        }

        BoxType? box = null;
        if (request.BoxTypeId.HasValue)
        {
            box = await _dbContext.BoxTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.BoxTypeId && b.IsActive, cancellationToken);
            if (box is null)
            {
                throw new InvalidOperationException("InvalidBoxType");
            }
        }

        var validPhotoIds = await _dbContext.Photos
            .Where(photo => photo.BookingId == request.BookingId && request.PhotoIds.Contains(photo.Id))
            .Select(photo => photo.Id)
            .ToListAsync(cancellationToken);

        if (validPhotoIds.Count != request.PhotoIds.Count)
        {
            throw new InvalidOperationException("InvalidPhotos");
        }

        var totalPrice = album.BasePrice + (box?.BasePrice ?? 0m);

        var order = new Order
        {
            BookingId = request.BookingId,
            ClientId = request.ClientId,
            AlbumTypeId = album.Id,
            BoxTypeId = box?.Id,
            TotalPrice = totalPrice
        };

        _dbContext.Orders.Add(order);
        foreach (var photoId in validPhotoIds)
        {
            order.OrderPhotos.Add(new OrderPhoto { Order = order, PhotoId = photoId });
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new OrderDto
        {
            Id = order.Id,
            BookingId = order.BookingId,
            ClientId = order.ClientId,
            AlbumTypeId = order.AlbumTypeId,
            BoxTypeId = order.BoxTypeId,
            Status = order.Status,
            TotalPrice = order.TotalPrice
        };
    }
}
