using Microsoft.Extensions.DependencyInjection;
using NamasStudio.DataAccess.Models;
using NamasStudio.Repository.Repositories.Implementations;
using NamasStudio.Repository.Repositories.Interfaces;

namespace NamasStudio.Repository {
    public static class DependencyInjection {

        public static void AddRepositoryService(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<NamasStudioContext>();
            services.AddTransient<ICategoryProductRepo, CategoryProductRepo>();
            services.AddTransient<IProductsRepo, ProductsRepo>();
            services.AddTransient<IProductStockRepo, ProductStockRepo>();
            services.AddTransient<IProductSizeRepo, ProductSizeRepo>();
            services.AddTransient<IProductPhotoRepo, ProductPhotoRepo>();
            services.AddTransient<IAuthRepo, AuthRepo>();
        }
    }
}
