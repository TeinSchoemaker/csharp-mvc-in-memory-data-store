using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public interface IProductRespository
    {
        Task<Product> CreateProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int id);
    }
}
