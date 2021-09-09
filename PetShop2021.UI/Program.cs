using PetShop2021.Core.IServices;
using Microsoft.Extensions.DependencyInjection;
using PetShop2021.Domain.IRepositories;
using PetShop2021.Domain.Services;
using PetShop2021.Infrastructure.DataAccess.Repositories;
using PetShop2021.SQL.Repositories;

namespace PetShop2021.UI {
    class Program {
        static void Main(string[] args) {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepositoryInMemory>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IPetTypeRepository, PetTypeRepository>();
            serviceCollection.AddScoped<IPetTypeService, PetTypeService>();
           
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IPetService>();
            var petTypeService = serviceProvider.GetRequiredService<IPetTypeService>();
            var menu = new Menu(service,petTypeService);
            menu.Start();
        }
    }
}