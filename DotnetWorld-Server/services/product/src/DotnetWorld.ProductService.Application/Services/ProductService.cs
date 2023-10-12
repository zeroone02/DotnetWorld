using AutoMapper;
using DotnetWorld.CouponService.Application;
using DotnetWorld.DDD;
using DotnetWorld.ProductService.Application.Contracts;
using DotnetWorld.ProductService.Domain;
namespace DotnetWorld.ProductService.Application;
public class ProductService :
    CrudAppService<Product, ProductDto, Guid, CreateProductDto, UpdateProductDto>,
    IProductService
{
    private readonly IRepository<Product, Guid> _repository;
    private readonly UnitOfWork _unitOfWork;
    public ProductService(IServiceProvider serviceProvider,
        IMapper mapper, IRepository<Product, Guid> repository, UnitOfWork unitOfWork) 
        : base(serviceProvider, mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public override Task<ProductDto> CreateAsync(CreateProductDto input)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto input,string inputBaseUrl)
    {
        Product product = ObjectMapper.Map<CreateProductDto, Product>(input);
        await _repository.InsertAsync(product);
        await _unitOfWork.SaveChangesAsync();
        if (input.Image != null)
        {
            string fileName = product.Id + Path.GetExtension(input.Image.FileName);
            string filePath = @"wwwroot\ProductImages\" + fileName;

            //Я добавил условие if, чтобы удалить любое изображение с таким же именем, если оно существует в папке, путем любого изменения
            var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            FileInfo file = new FileInfo(directoryLocation);
            if (file.Exists)
            {
                file.Delete();
            }

            var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
            {
                input.Image.CopyTo(fileStream);
            }
            product.SetImageUrl(inputBaseUrl + "/ProductImages/" + fileName)
                   .SetImageLocalPath(filePath);
        }
        else
        {
            product.SetImageUrl("https://placehold.co/600x400");
        }
        await _repository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Product, ProductDto>(product);
    }

    public async override Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetAsync(id);
        if (!string.IsNullOrEmpty(product.ImageLocalPath))
        {
            var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
            FileInfo file = new FileInfo(oldFilePathDirectory);
            if (file.Exists)
            {
                file.Delete();
            }
        }
        await _repository.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async override Task<ProductDto> GetAsync(Guid id)
    {
        Product product = await _repository.GetAsync(id);
        if (product == null)
        {
            throw new Exception($"Продукт с id = {id} не найден");
        }
        return ObjectMapper.Map<Product, ProductDto>(product);
    }

   

    public async override Task<List<ProductDto>> GetListAsync()
    {
        List<Product> products = await _repository.GetListAsync();
        if (products == null)
        {
            throw new Exception("Продукты не найдены");
        }
        var result = ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
        return result;

    }

    public override Task<ProductDto> UpdateAsync(UpdateProductDto input)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductDto> UpdateProductAsync(UpdateProductDto input, string inputBaseUrl)
    {
        Product product = ObjectMapper.Map<UpdateProductDto, Product>(input);

        if (input.Image != null)
        {
            if (!string.IsNullOrEmpty(product.ImageLocalPath))
            {
                var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                FileInfo file = new FileInfo(oldFilePathDirectory);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            string fileName = product.Id + Path.GetExtension(input.Image.FileName);
            string filePath = @"wwwroot\ProductImages\" + fileName;
            var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
            {
                input.Image.CopyTo(fileStream);
            }
            product.SetImageUrl(inputBaseUrl + "/ProductImages/" + fileName)
                   .SetImageLocalPath(filePath);

            await _repository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            
        }
        return ObjectMapper.Map<Product, ProductDto>(product);
    }
}
