using Microservice.Catalog.Api.Features.Categories.GetById;
using Microservice.Catalog.Api.Features.Courses.Dtos;

namespace Microservice.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

    public class GetCourseByIdHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (hasCourse == null)
            {
                return ServiceResult<CourseDto>.Error("Course not found", $"The course with Id {request.Id} was not found", HttpStatusCode.NotFound);
            }

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == hasCourse.CategoryId, cancellationToken);

            hasCourse.Category = category;

            var courseAsDto = mapper.Map<CourseDto>(hasCourse);
            return ServiceResult<CourseDto>.SuccessAsOk(courseAsDto);
        }
    }
    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
            (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult()).MapToApiVersion(1.0)
            .WithName("GetByIdCourse");

            return group;
        }
    }
}
