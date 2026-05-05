using EventsAPI.Application.Reviews.Models;
using EventsAPI.Application.Reviews.Queries;
using EventsAPI.Infrastructure.Persistence;
using EventsAPI.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Application.Reviews.Handlers;

public class GetPhotographerReviewsHandler : IRequestHandler<GetPhotographerReviewsQuery, PagedResult<ReviewDto>>
{
    private readonly AppDbContext _dbContext;

    public GetPhotographerReviewsHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<ReviewDto>> Handle(GetPhotographerReviewsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Reviews.Where(review => review.PhotographerId == request.PhotographerId);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderByDescending(review => review.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(review => new ReviewDto
            {
                Id = review.Id,
                BookingId = review.BookingId,
                PhotographerId = review.PhotographerId,
                ClientId = review.ClientId,
                Rating = review.Rating,
                Comment = review.Comment
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<ReviewDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}
