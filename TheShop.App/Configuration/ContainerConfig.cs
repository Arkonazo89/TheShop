using Autofac;
using System.Reflection;
using System.Linq;
using TheShop.Infrastructure;

namespace TheShop.App
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<ShopService>().As<IShopService>();

            builder.RegisterAssemblyTypes(Assembly.Load("TheShop.Infrastructure"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}
