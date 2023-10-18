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
    private readonly IMapper ObjectMapper;
    private readonly ShoppingCartServiceDbContext _db;
    private readonly IProductService _productService;
    private readonly ICouponService _couponService;
    public CartService(UnitOfWork unitOfWork, IMapper objectMapper, ShoppingCartServiceDbContext db, IProductService productService, ICouponService couponService)
    {
        _unitOfWork = unitOfWork;
        ObjectMapper = objectMapper;
        _db = db;
        _productService = productService;
        _couponService = couponService;
    }
    public async Task<bool> ApplyCoupon(CartDto cartDto)
    {
        var userCart = _db.UserCarts.First(cart => cart.UserId == cartDto.UserCart.UserId);
        userCart.CouponCode = cartDto.UserCart.CouponCode;
        _db.UserCarts.Update(userCart);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<CartDto> CartUpsert(CartDto cartDto)
    {
       var userCart = await _db.UserCarts.AsNoTracking()
            .FirstOrDefaultAsync(cart => cart.UserId == cartDto.UserCart.UserId);
        if (userCart == null)
        {
            UserCart newUserCart = ObjectMapper.Map<UserCart>(cartDto.UserCart);

            _db.UserCarts.Add(newUserCart);

            await _unitOfWork.SaveChangesAsync();

            cartDto.CartDetails.First().UserCartId = newUserCart.Id;

            _db.CartDetails.Add(ObjectMapper.Map<CartDetail>(cartDto.CartDetails.First()));
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            var cartDetail = await _db.CartDetails.AsNoTracking()
                .FirstOrDefaultAsync(detail => detail.ProductId == cartDto.CartDetails.First().ProductId
                && detail.UserCartId == userCart.Id);

            if (cartDetail == null)
            {
                cartDto.CartDetails.First().UserCartId = userCart.Id;
                _db.CartDetails.Add(ObjectMapper.Map<CartDetail>(cartDto.CartDetails.First()));
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                cartDto.CartDetails.First().Count += cartDetail.Count;
                cartDto.CartDetails.First().UserCartId = cartDetail.UserCartId;
                cartDto.CartDetails.First().Id = cartDetail.Id;

                _db.CartDetails.Update(ObjectMapper.Map<CartDetail>(cartDto.CartDetails.First()));
                await _unitOfWork.SaveChangesAsync();
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
        CartDetail cartDetail = _db.CartDetails
               .First(u => u.Id == cartDetailId);

        int totalCountofCartItem = _db.CartDetails
            .Where(u => u.UserCartId == cartDetail.UserCartId)
            .Count();
        _db.CartDetails.Remove(cartDetail);
        if (totalCountofCartItem == 1)
        {
            var cartHeaderToRemove = await _db.UserCarts
                   .FirstOrDefaultAsync(u => u.Id == cartDetail.UserCartId);
            _db.UserCarts.Remove(cartHeaderToRemove);
        }
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveCoupon(CartDto cartDto)
    {
        var userCart = _db.UserCarts.First(cart => cart.UserId == cartDto.UserCart.UserId);
        userCart.CouponCode = "";
        _db.UserCarts.Update(userCart);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
