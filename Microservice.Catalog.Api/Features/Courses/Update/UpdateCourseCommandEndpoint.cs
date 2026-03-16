using Microservice.Catalog.Api.Features.Courses.Create;
using Microservice.Shared.Filters;

namespace Microservice.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseCommandEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCourseCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("UpdateCourse")
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        } 
    }
}
