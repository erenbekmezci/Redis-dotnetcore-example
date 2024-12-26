using Microsoft.EntityFrameworkCore;
using redis_ornek.API;
using redis_ornek.API.Repository;
using redis_ornek.API.Services;
using redis_ornek.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ornekDb"));
builder.Services.AddScoped<IProductRepository>(sp =>
{
    var dbContext = sp.GetRequiredService<AppDbContext>();
    var productRepository = new ProductRepository(dbContext);
    var redis = sp.GetRequiredService<RedisService>();
    return new ProductRepositoryWithCache(productRepository, redis);
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<RedisService>(sp =>
{
    return new RedisService(builder.Configuration["CacheOptions:Url"]);
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

}


app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
