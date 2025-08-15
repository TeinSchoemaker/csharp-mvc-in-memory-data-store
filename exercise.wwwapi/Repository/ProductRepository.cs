using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Data;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Repository
{
    public class ProductRepository : IProductRespository
    {
        private DataContext _db;

        public ProductRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateProduct(Product entity)
        {
            await _db.Products.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var oldProduct = await _db.Products.FindAsync(product.Id);
            
            oldProduct.Name = product.Name;
            oldProduct.Category = product.Category;
            oldProduct.Price = product.Price;

            await _db.SaveChangesAsync();
            return oldProduct;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var target = await _db.Products.FindAsync(id);

            _db.Products.Remove(target);
            await _db.SaveChangesAsync();
            return target;
        }
    }
}
