using CatalogService.Application.DTOs;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using MapsterMapper;
using Microsoft.Extensions.Configuration;

namespace CatalogService.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ProductAppService(IProductRepository productRepository, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _productRepository = productRepository;
        }
        public void Add(ProductDTO product)
        {
            var entity = _mapper.Map<ProductDTO, Product>(product);
            _productRepository.Add(entity);
            _productRepository.SaveChanges();
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
            _productRepository.SaveChanges();
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var products = _productRepository.GetAll();
            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ProductDTO>();
            }

            products.ToList().ForEach(p =>
            {
                if (!string.IsNullOrEmpty(p.ImageUrl))
                {
                    p.ImageUrl = $"{_configuration["ImageAddress"]}{p.ImageUrl}";
                }
            });

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public ProductDTO GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                product.ImageUrl = $"{_configuration["ImageAddress"]}{product.ImageUrl}";
            }
            return _mapper.Map<Product, ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetByIds(int[] ids)
        {
            var products = _productRepository.GetByIds(ids);
            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ProductDTO>();
            }
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public void Update(ProductDTO product)
        {
            var entity = _mapper.Map<ProductDTO, Product>(product);
            _productRepository.Update(entity);
            _productRepository.SaveChanges();
        }
    }
}
