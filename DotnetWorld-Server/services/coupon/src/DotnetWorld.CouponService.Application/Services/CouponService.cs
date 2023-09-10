using AutoMapper;
using DotnetWorld.CouponService.Application.Contracts;
using DotnetWorld.CouponService.Domain;
using DotnetWorld.DDD;
using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.CouponService.Application;
public class CouponService :
    CrudAppService<Coupon, CouponDto, Guid, CreateCouponDto, UpdateCouponDto, PagedRequestDto>,
    ICouponService
{
    private readonly IRepository<Coupon,Guid> _repository;
    private readonly UnitOfWork _unitOfWork;
    public CouponService(IServiceProvider serviceProvider, IMapper mapper, IRepository<Coupon, Guid> repository, UnitOfWork unitOfWork) : base(serviceProvider, mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async override Task<CouponDto> CreateAsync(CreateCouponDto input)
    {
        throw new NotImplementedException();
    }

    public async override Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async override Task<CouponDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<CouponDto> GetByCodeAsync(string code)
    {
        throw new NotImplementedException();
    }

    public async override Task<PagedResultDto<CouponDto>> GetListAsync(PagedRequestDto input)
    {
        throw new NotImplementedException();
    }

    public async override Task<CouponDto> UpdateAsync(Guid id, UpdateCouponDto input)
    {
        throw new NotImplementedException();
    }
}
