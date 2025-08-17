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

            products.MapPost("/{product}", CreateProduct);
            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id}", GetProductsById);
            products.MapPut("/{product}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateProduct(IProductRepository repository, string name, string category, int price)
        {
            var newProduct = new Product
            {
                Name = name,
                Category = category,
                Price = price,
            };

            await repository.CreateProduct(newProduct);

            return TypedResults.Created($"/products/{newProduct.Id}", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllProducts(IProductRepository repository)
        {
            var results = await repository.GetAllProducts();
            return TypedResults.Ok(results);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProductsById(IProductRepository repository, int id)
        {
            var result = await repository.GetProductById(id);
            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, string name, int id, string category, int price)
        {
            var newProduct = new Product
            {
                Id = id,
                Name = name,
                Category = category,
                Price = price,
            };

            var updated = await repository.UpdateProduct(newProduct);
            return TypedResults.Ok(updated);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {
            var deleted = await repository.DeleteProduct(id);
            return TypedResults.Ok(deleted);
        }
    }
}
