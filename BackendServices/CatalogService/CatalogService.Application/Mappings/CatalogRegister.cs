using CatalogService.Application.DTOs;
using CatalogService.Domain.Entities;
using Mapster;

namespace CatalogService.Application.Mappings
{
    public class CatalogRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductDTO>().TwoWays();
        }
    }
}
