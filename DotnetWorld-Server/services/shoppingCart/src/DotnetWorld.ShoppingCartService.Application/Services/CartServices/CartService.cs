using AutoMapper;
using DotnetWorld.DDD;
using DotnetWorld.ShoppingCartService.Application.Contracts;
using DotnetWorld.ShoppingCartService.Domain;

namespace DotnetWorld.ShoppingCartService.Application;
public class CartService : ICartService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRepository<UserCart, Guid> _repository;
    public CartService(UnitOfWork unitOfWork, IMapper mapper, IRepository<UserCart, Guid> repository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
    }
    public Task<CartDto> ApplyCoupon(CartDto cartDto)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto> CartUpsert(CartDto cartDto)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto> GetCart(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveCart(Guid cartDetailId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveCoupon(CartDto cartDto)
    {
        throw new NotImplementedException();
    }
}
