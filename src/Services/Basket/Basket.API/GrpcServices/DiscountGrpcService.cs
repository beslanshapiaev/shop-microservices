using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;


public class DiscountGrpcService (DiscountProtoService.DiscountProtoServiceClient discountProtoService)
{
    readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService = discountProtoService;

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };

        return await _discountProtoService.GetDiscountAsync(discountRequest);
    }
}
