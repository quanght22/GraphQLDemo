using App.Infrastructure;
using App.Services.Contracts;
using App.Services.Implementation;
using Autofac;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Services
{
    public class DependencyInjectionInstaller : Module
    {
        public DbContextOptions<ApplicationDbContext> option { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new App.Data.DependencyInjectionInstaller()
            {
                option = this.option
            }); ;
            InstallerServices(builder);
        }
        private void InstallerServices(ContainerBuilder builder)
        {
            builder.RegisterType<NoteService>().As<INoteService>();
        }
    }
}
