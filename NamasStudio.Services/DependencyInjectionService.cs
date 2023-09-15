using Microsoft.Extensions.DependencyInjection;
using NamasStudio.Repository;
using NamasStudio.Services.Services.Implementations;
using NamasStudio.Services.Services.Interfaces;


namespace NamasStudio.Service {
    public static class DependencyInjectionService {
        public static void AddServices(IServiceCollection services)
        {
            DependencyInjection.AddRepositoryService(services);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();
            services.AddTransient<ICategoryProductService, CategoryProductService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IProductStockService, ProductStockService>();
            services.AddTransient<IProductSizeService, ProductSizeService>();
            services.AddTransient<IProductPhotoService, ProductPhotoService>();
            services.AddTransient<IAuthService, AuthService>();
            //services.AddHttpClient();

        }
    }
}
