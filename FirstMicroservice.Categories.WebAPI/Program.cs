using FirstMicroservice.Categories.WebAPI.Context;
using FirstMicroservice.Categories.WebAPI.Dtos;
using FirstMicroservice.Categories.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

app.MapGet("/categories/getall", async(ApplicationDbContext context,CancellationToken token) => { 
    var categories=await context.Categories.ToListAsync();
    return categories;
});

app.MapPost("/categories/create", async (CreateCategoryDto dto, ApplicationDbContext context, CancellationToken token) =>
{
    bool isNameExist=await context.Categories.AnyAsync(c=>c.Name==dto.Name,cancellationToken:token);

    if (isNameExist){ return Results.BadRequest(new { Message = "Category already exist" }); } 

    Category category = new()
    {
        Name = dto.Name
    };

    context.Categories.AddAsync(category,token);
    context.SaveChangesAsync(token);

    return Results.Ok( new { Message="Category is created successfully" } );
});

app.Run();
