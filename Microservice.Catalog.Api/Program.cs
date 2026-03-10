using MediatR;
using Microservice.Catalog.Api;
using Microservice.Catalog.Api.Features.Categories;
using Microservice.Catalog.Api.Features.Categories.Create;
using Microservice.Catalog.Api.Options;
using Microservice.Catalog.Api.Repositories;
using Microservice.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));


var app = builder.Build();


app.AddCategoryGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();