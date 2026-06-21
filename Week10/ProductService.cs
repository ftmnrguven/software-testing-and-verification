public class ProductService
{
    private AppDbContext dbContext;

    public ProductService(AppDbContext dbContext)
    {
        this.dbContext= dbContext;
    }


    public async Task<List<Product>> GetAllProductAsync()
    {
        return await dbContext.Products.ToListAsync<Product>();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }



    public async Task AddProductAsync(Product p1)
    {
        await dbContext.Products.AddAsync(p1);
        await dbContext.SaveChangesAsync();
    }


    public async Task<bool> DeleteProductByIdAsync(int id)
    {

        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return false;

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return true;


    }
}