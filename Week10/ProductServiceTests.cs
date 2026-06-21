public class ProductServiceTests
{

    private readonly ITestOutputHelper output;

    public ProductServiceTests(ITestOutputHelper output)
    {
       this.output = output;
    }

    private ProductService CreateProductService()
    {
    //Aşağıdaki kodla birlikte ProductService her oluşturulduğunda farklı bir veritabanı oluşturulacak.
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        return new ProductService(context);
    }


    private Product generateRandomProduct()
    {
        Random rnd = new Random();
        return new Product { Name = $"Test Product-{rnd.Next(0, 100)}", Price = rnd.Next(0, 100) };
    }

    [Fact]
    public async Task AddProductAsync_Should_Add_Product_To_Database()
    {
        var productService = CreateProductService();
        var product1 = new Product { Name = "Test Product-1", Price = 13 };

        await productService.AddProductAsync(product1);


        var allProducts = await productService.GetAllProductAsync();


        Assert.Single(allProducts);//Yöntem-1:allProducts Listesinde 1 tane mi eleman var
        Assert.Equal(1,allProducts.Count);//Yöntem-2:allProducts Listesinde 1 tane mi eleman var
        Assert.Equal(product1.Name, allProducts[0].Name);
        Assert.Equal(product1.Price, allProducts[0].Price);

    }


    [Fact]
    public async Task GetAllProductsAsync_Should_Return_All_Products()
    {
        var productService = CreateProductService();

        var product1 = new Product { Name = "Test Product-1", Price = 12 };
        var product2 = new Product { Name = "Test Product-2", Price = 13 };

        
        await productService.AddProductAsync(product1);
        await productService.AddProductAsync(product2);
        

        var allProducts = await productService.GetAllProductAsync();
        Assert.Equal(2, allProducts.Count);
    }


    [Fact]
    public async Task GetProductByIdAsync_Should_Return_Correct_Product()
    {
        var productService = CreateProductService();
        var product1 = generateRandomProduct();
        var product2 = generateRandomProduct();


        output.WriteLine(product1.Id.ToString());
        await productService.AddProductAsync(product1);
        output.WriteLine(product1.Id.ToString());


        await productService.AddProductAsync(product2);


        var retrievedProduct = await productService.GetProductByIdAsync(product1.Id);
        Assert.NotNull(retrievedProduct);
        Assert.True(product1.Id == retrievedProduct.Id);    


    }

    [Fact]
    public async Task GetProductByIdAsync_Should_Return_Null_If_Not_Found()
    {
        var productService = CreateProductService();
        var retrievedProduct = await productService.GetProductByIdAsync(999);
        Assert.Null(retrievedProduct);
    }
    [Fact]

    public async Task DeleteProductByIdAsync_Should_Delete_Product_If_Exists()
    {
        Product p1 = generateRandomProduct();
        ProductService productService = CreateProductService();

        await productService.AddProductAsync(p1);

        bool result = await productService.DeleteProductByIdAsync(p1.Id);
        Assert.True(result);

    }

    [Fact]
    public async Task DeleteProductByIdAsync_Should_Return_False_If_Product_Not_Found()
    {
        var productService = CreateProductService();
        bool result = await productService.DeleteProductByIdAsync(100);
        Assert.False(result);

    }
}

