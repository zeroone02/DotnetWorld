using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.CouponService.Application.Contracts;
public interface ICouponService :
     ICrudAppService<CouponDto, Guid, CreateCouponDto, UpdateCouponDto>
{
    public Task<CouponDto> GetByCodeAsync(string code);
}
