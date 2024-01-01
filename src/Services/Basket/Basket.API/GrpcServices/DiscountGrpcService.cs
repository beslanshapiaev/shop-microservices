using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;


public class DiscountGrpcService (DiscountProtoService.DiscountProtoServiceClient discountProtoService, IConfiguration config, ILogger<DiscountGrpcService> logger)
{
    readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService = discountProtoService;
    readonly IConfiguration _config = config;
    readonly ILogger<DiscountGrpcService> _logger = logger;

    public async Task<CouponModel> GetDiscount(string productName)
    {
        _logger.LogInformation($"Connection String = {_config["GrpcSettings:DiscountUrl"]}");

        var discountRequest = new GetDiscountRequest { ProductName = productName };

        return await _discountProtoService.GetDiscountAsync(discountRequest);
    }
}
