using App.Data.Contracts;
using App.Infrastructure;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class DependencyInjectionInstaller : Module
    {
        // Built-in Dependency Injection( Not Work)
        //public static IServiceCollection InstallerRepoServices(this IServiceCollection services, DbContextOptions<ApplicationDbContext> option)
        //{
        //    services.InstallerDBContexts();
        //    services.AddScoped<IUnitOfWork, UnitOfWork>();

        //    var assembly = Assembly.LoadFile( Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"App.Data.dll"));
        //    var types = assembly.GetTypes().Where(a => a.Name.EndsWith("Repository") && !a.IsAbstract && !a.IsInterface)
        //                       .Select(a => new { assignedType = a, serviceType = a.GetInterfaces().FirstOrDefault(x => x.Name.Contains(a.Name))}).ToList();
        //    //types.ForEach(typeToRegister =>
        //    //{
        //    //    services.AddTransient(typeToRegister.serviceType, typeToRegister.assignedType);

        //    //});
        //    services.AddTransient<NoteRepository>();
        //    services.AddTransient<INoteRepository>((sp) =>
        //    {
        //        Type type = Type.GetType("NoteRepository", true);
        //        object obj = Activator.CreateInstance(type);
        //        PropertyInfo prop = type.GetProperty("DbContextProvider");
        //        prop.SetValue(obj, new DbContextProvider(option));
        //        return (NoteRepository)obj;
        //    });
        //    return services;
        //}
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
        }
    }

}
