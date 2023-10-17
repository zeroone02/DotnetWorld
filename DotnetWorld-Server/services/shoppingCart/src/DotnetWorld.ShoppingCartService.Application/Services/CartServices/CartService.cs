using AutoMapper;
using DotnetWorld.DDD;
using DotnetWorld.ShoppingCartService.Application.Contracts;
using DotnetWorld.ShoppingCartService.Domain;
using DotnetWorld.ShoppingCartService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace DotnetWorld.ShoppingCartService.Application;
public class CartService : ICartService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ShoppingCartServiceDbContext _db;
    public CartService(UnitOfWork unitOfWork, IMapper mapper, ShoppingCartServiceDbContext db)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _db = db;
    }
    public async Task<bool> ApplyCoupon(CartDto cartDto)
    {
        var cartFromDb = _db.UserCarts.First(u => u.UserId == cartDto.UserCart.UserId);
        cartFromDb.CouponCode = cartDto.UserCart.CouponCode;
        _db.UserCarts.Update(cartFromDb);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<CartDto> CartUpsert(CartDto cartDto)
    {
        var userCartsFromDb = await _db.UserCarts.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == cartDto.UserCart.UserId);
        if (userCartsFromDb == null)
        {
            //create header and details
            UserCart userCart = _mapper.Map<UserCart>(cartDto.UserCart);
            _db.UserCarts.Add(userCart);
            await _db.SaveChangesAsync();
            cartDto.CartDetails.First().UserCartId = userCart.Id;
            _db.CartDetails.Add(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
            await _db.SaveChangesAsync();
        }
        else
        {
            //if header is not null
            //check if details has same product
            var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                u => u.ProductId == cartDto.CartDetails.First().ProductId &&
                u.UserCartId == userCartsFromDb.Id);
            if (cartDetailsFromDb == null)
            {
                //create cartdetails
                cartDto.CartDetails.First().UserCartId = userCartsFromDb.Id;
                _db.CartDetails.Add(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
                await _db.SaveChangesAsync();
            }
            else
            {
                //update count in cart details
                cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                cartDto.CartDetails.First().UserCartId = cartDetailsFromDb.UserCartId;
                cartDto.CartDetails.First().Id = cartDetailsFromDb.Id;
                _db.CartDetails.Update(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
                await _db.SaveChangesAsync();
            }
        }
        return cartDto;
    }
    public async Task<CartDto> GetCart(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveCart(Guid cartDetailId)
    {
        CartDetail cartDetails = _db.CartDetails
               .First(u => u.Id == cartDetailId);

        int totalCountofCartItem = _db.CartDetails
            .Where(u => u.UserCartId == cartDetails.UserCartId)
            .Count();
        _db.CartDetails.Remove(cartDetails);
        if (totalCountofCartItem == 1)
        {
            var cartHeaderToRemove = await _db.UserCarts
                   .FirstOrDefaultAsync(u => u.Id == cartDetails.UserCartId);
            _db.UserCarts.Remove(cartHeaderToRemove);
        }
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveCoupon(CartDto cartDto)
    {
        var cartFromDb = _db.UserCarts.First(u => u.UserId == cartDto.UserCart.UserId);
        cartFromDb.CouponCode = "";
        _db.UserCarts.Update(cartFromDb);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
