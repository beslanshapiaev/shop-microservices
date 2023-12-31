using Discount.Grpc.Extensions;
using Discount.Grpc.Mapper;
using Discount.Grpc.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(DiscountProfile));

var app = builder.Build();


// Configure the HTTP request pipeline.
app.Services.MigrateDatabase<Program>(0);
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
