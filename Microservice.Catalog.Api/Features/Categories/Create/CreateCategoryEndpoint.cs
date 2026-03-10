using MediatR;
using Microservice.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            //http://localhost:5000/api/categories
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult());

            return group;
        }
    }
}
