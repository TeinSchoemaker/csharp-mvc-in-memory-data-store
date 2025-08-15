using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.DTO;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapPost("/", CreateProduct);
            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id}", GetProductsById);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateProduct(IProductRespository repository, NewProduct product)
        {
            Product entity = new Product();
            entity.Name = product.Name;
            entity.Category = product.Category;
            entity.Price = product.Price;

            await repository.CreateProduct(entity);

            return TypedResults.Created($"https://localhost:7197/products/{entity.Id}", entity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllProducts(IProductRespository repository)
        {
            var results = await repository.GetAllProducts();
            return TypedResults.Ok(results);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProductsById(IProductRespository repository, int id)
        {
            var result = await repository.GetProductById(id);
            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateProduct(IProductRespository repository, int id, Product product)
        {
            var updated = await repository.UpdateProduct(product);
            return TypedResults.Created($"https://localhost:7197/products/{updated.Id}" , updated);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(IProductRespository repository, int id)
        {
            var deleted = await repository.DeleteProduct(id);
            return TypedResults.Ok(deleted);
        }
    }
}
