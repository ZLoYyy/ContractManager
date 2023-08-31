using ContractManager.Data.Interfaces;
using ContractManager.Data.Repositories;
using ContractManager.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ContractManager.Data
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
           .AddTransient<IRepository<Contract>, ContractRepository>()
           .AddTransient<IRepository<ContractStage>, ContractStageRepository>();
    }
}
