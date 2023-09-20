using AutoMapper;
using DotnetWorld.CouponService.Application.Contracts;
using DotnetWorld.CouponService.Domain;
using DotnetWorld.DDD;
using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.CouponService.Application;
public class CouponService :
    CrudAppService<Coupon, CouponDto, Guid, CreateCouponDto, UpdateCouponDto>,
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
        Coupon coupon = ObjectMapper.Map<CreateCouponDto, Coupon>(input);
        await _repository.InsertAsync(coupon);
        await _unitOfWork.SaveChangesAsync();
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    public async override Task DeleteAsync(Guid id)
    {
        var coupon = await _repository.GetAsync(id);
        await _repository.DeleteAsync(coupon);
        await _unitOfWork.SaveChangesAsync();
    }

    public async override Task<CouponDto> GetAsync(Guid id)
    {
        Coupon coupon = await _repository.GetAsync(id);
        if (coupon == null)
        {
            throw new Exception($"Купон с id = {id} не найден");
        }
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    public async Task<CouponDto> GetByCodeAsync(string code)
    {

        Coupon coupon = _repository.GetQueryable()
            .FirstOrDefault(x => x.CouponCode == code);

        if (coupon == null)
        {
            throw new Exception($"Купон с code = {code} не найден");
        }
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    public async override Task<List<CouponDto>> GetListAsync()
    {
        List<Coupon> coupons = await _repository.GetListAsync();
        if (coupons == null)
        {
            throw new Exception("Данные не найдены");
        }
        var result = ObjectMapper.Map<List<Coupon>, List<CouponDto>>(coupons);
        return result;
       
    }

    public async override Task<CouponDto> UpdateAsync(UpdateCouponDto input)
    {
        Coupon coupon = ObjectMapper.Map<UpdateCouponDto, Coupon>(input);
        await _repository.UpdateAsync(coupon);
        await _unitOfWork.SaveChangesAsync();
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }
}
