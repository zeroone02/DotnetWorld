using AutoMapper;
using DotnetWorld.DDD;
using DotnetWorld.ShoppingCartService.Application.Contracts;
using DotnetWorld.ShoppingCartService.Domain;
using DotnetWorld.ShoppingCartService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            UserCart newUserCart = ObjectMapper.Map<UserCartDto, UserCart>(cartDto.UserCart);

            _db.UserCarts.Add(newUserCart);

            await _unitOfWork.SaveChangesAsync();

            cartDto.CartDetails.First().UserCartId = newUserCart.Id;

            _db.CartDetails.Add(ObjectMapper.Map<CartDetailDto,CartDetail>(cartDto.CartDetails.First()));
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
                _db.CartDetails.Add(ObjectMapper.Map<CartDetailDto, CartDetail>(cartDto.CartDetails.First()));
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                cartDto.CartDetails.First().Count += cartDetail.Count;
                cartDto.CartDetails.First().UserCartId = cartDetail.UserCartId;
                cartDto.CartDetails.First().Id = cartDetail.Id;

                _db.CartDetails.Update(ObjectMapper.Map<CartDetailDto, CartDetail>(cartDto.CartDetails.First()));
                await _unitOfWork.SaveChangesAsync();
            }

        }
        return cartDto;
    }
    public async Task<CartDto> GetCart(string userId)
    {
        CartDto cartDto = new CartDto()
        {
            UserCart = ObjectMapper.Map<UserCart, UserCartDto>(_db.UserCarts.First(cart => cart.UserId == userId)),
        };

        cartDto.CartDetails = ObjectMapper.Map<IEnumerable<CartDetail>, IEnumerable<CartDetailDto>>(_db.CartDetails
                .Where(cartDetail => cartDetail.UserCartId == cartDto.UserCart.Id));

        IEnumerable<ProductDto> productDtos = await _productService.GetProducts();

        foreach (var cartDetail in cartDto.CartDetails)
        {
            cartDetail.Product = productDtos.FirstOrDefault(product => product.Id == cartDetail.ProductId);
            if (cartDetail.Product == null)
            {
                await RemoveCart(cartDetail.Id);
            }
        }

        CartDto newCart = new()
        {
            UserCart = ObjectMapper.Map<UserCart,UserCartDto>(_db.UserCarts.First(u => u.UserId == userId))
        };

        newCart.CartDetails = ObjectMapper.Map<IEnumerable<CartDetail>, IEnumerable<CartDetailDto>>(_db.CartDetails
               .Where(cartdetail => cartdetail.UserCartId == newCart.UserCart.Id));
        foreach (var newCartDetail in newCart.CartDetails)
        {
            newCartDetail.Product = productDtos.FirstOrDefault(u => u.Id == newCartDetail.ProductId);
            newCart.UserCart.CartTotal += (newCartDetail.Count * newCartDetail.Product.Price);
        }

        if (!string.IsNullOrEmpty(newCart.UserCart.CouponCode))
        {
            CouponDto coupon = await _couponService.GetCoupon(newCart.UserCart.CouponCode);
            if (coupon != null && newCart.UserCart.CartTotal > coupon.MinAmount)
            {
                newCart.UserCart.CartTotal -= coupon.DiscountAmount;
                newCart.UserCart.Discount = coupon.DiscountAmount;
            }
        }
        return newCart;
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
