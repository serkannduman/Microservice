using MassTransit;
using MediatR;
using Microservice.Catalog.Api.Repositories;
using Microservice.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Microservice.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (existCategory)
                return ServiceResult<CreateCategoryResponse>.Error("Category already exist", $"The category name '{request.Name} already exist'", HttpStatusCode.BadRequest);

            var category = new Category
            {
                Id = NewId.NextSequentialGuid(),
                Name = request.Name
            };

            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), "<empty>");
        }
    }
}
