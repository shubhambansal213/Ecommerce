using System.Reflection;
using Catalog.Application;
using Catalog.Core;
using Catalog.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Add swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Mediatr
var assemblies=new Assembly[]
{
  Assembly.GetExecutingAssembly(),
  typeof(GetAllBrandsHandler).Assembly  
};
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(assemblies));
 
//customer services
builder.Services.AddScoped<IBrandRepository,BrandRepository>();
builder.Services.AddScoped<ITypeRepository,TypeRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();

//Build strongly-typed setting
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings")
);

//Register MongoClient as singleton
builder.Services.AddSingleton<IMongoClient>(sp =>
{
   var setting=sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
   return new MongoClient(setting.ConnectionString); 
});

var app = builder.Build();

//Seed MongoDb on Startup
using(var scope = app.Services.CreateScope())
{
    var options=scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>();
    await DatabaseSeeder.SeedAsync(options);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Enable Swagger
app.UseSwagger();
app.UseSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

