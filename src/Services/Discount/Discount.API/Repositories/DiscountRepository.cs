using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository(IConfiguration configuration) : IDiscountRepository
{
    readonly IConfiguration _configuration = configuration;

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var affected =
            await connection.ExecuteAsync
                ("insert into Coupon (ProductName, Description, Amount) values (@ProductName, @Description, @Amount)",
                new { coupon.ProductName, coupon.Description, coupon.Amount });
        
        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected =
            await connection.ExecuteAsync
                ("delete from coupon where ProductName = @ProductName",
                new { ProductName = productName });

        return affected != 0;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

        return coupon ?? new() { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
       
        var affected =
            await connection.ExecuteAsync
                ("update coupon set ProductName=@ProductName, Description=@Description, Amount=@Amount where Id = @Id",
                new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id});

        return affected != 0;
    }
}
