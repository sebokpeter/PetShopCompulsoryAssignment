using Microsoft.Extensions.DependencyInjection;
using PetShop;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Implementation;
using PetShop.Core.DomainService;
using PetShop.Infrastructure.Data;
using System;

namespace CompulsoryAssignment_PetShop
{
    class Program
    {
        static void Main(string[] args)
        {
            FakeDB.InitData();

            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IOwnerRepository, OwnerRepository>();
                
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IPetService petService = serviceProvider.GetRequiredService<IPetService>();

            Menu menu = new Menu(petService);
            menu.Run();
        }
    }
}
