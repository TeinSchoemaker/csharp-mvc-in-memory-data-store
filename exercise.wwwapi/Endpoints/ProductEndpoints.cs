using exercise.wwwapi.DTO;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct(IProductRepository repository, string name, string category, int price)
        {
            var productsList = await repository.GetAllProducts();

            if (!(price is int) || productsList.Any(x => x.Name.Equals(name)))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists.");
            }

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllProducts(IProductRepository repository)
        {
            var results = await repository.GetAllProducts();

            if (!results.Any())
            {
                return TypedResults.NotFound("No products of provided category were found");
            }

            return TypedResults.Ok(results);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductsById(IProductRepository repository, int id)
        {
            var result = await repository.GetProductById(id);

            if (result == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, string name, int id, string category, int price)
        {
            var result = await repository.GetProductById(id);

            if (result == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            var productsList = await repository.GetAllProducts();

            if (!(price is int) || productsList.Any(x => x.Name.Equals(name)))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists.");
            }

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {

            var result = await repository.GetProductById(id);

            if (result == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            var deleted = await repository.DeleteProduct(id);

            return TypedResults.Ok(deleted);
        }
    }
}
