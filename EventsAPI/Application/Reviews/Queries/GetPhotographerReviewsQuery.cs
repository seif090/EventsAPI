using EventsAPI.Application.Reviews.Models;
using EventsAPI.Shared;
using MediatR;

namespace EventsAPI.Application.Reviews.Queries;

public class GetPhotographerReviewsQuery : IRequest<PagedResult<ReviewDto>>
{
    public Guid PhotographerId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
