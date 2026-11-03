using Basket.Application.GrpcService;
using Basket.Application.Handlers;
using Basket.Application.Settings;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Basket.Infrastructure.Settings;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddOpenApi();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//Add swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register mediator services
var assemblies = new Assembly[]
    {
       Assembly.GetExecutingAssembly(),
       typeof(CreateShoppingCartHandler).Assembly
    };

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

//Options patterns
builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection("CacheSettings"));

builder.Services.Configure<GrpcSettings>(
    builder.Configuration.GetSection("GrpcSettings"));

//Register Grpc Client using IOptions
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    (sp, cfg) =>
{
    var grpcSettings = sp.GetRequiredService<IOptions<GrpcSettings>>().Value;
    cfg.Address = new Uri(grpcSettings.DiscountUrl);
});

//GRPC Service
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    (sp, cfg) =>
    {
        var grpcSettings = sp.GetRequiredService<IOptions<GrpcSettings>>().Value;
        cfg.Address = new Uri(grpcSettings.DiscountUrl);
    }
    );

//Redis
builder.Services.AddStackExchangeRedisCache((options) =>
{
    options.Configuration = builder.Configuration.GetSection("CacheSettings").GetValue<string>("ConnectionString");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();

app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
