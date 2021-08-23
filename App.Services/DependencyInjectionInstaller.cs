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
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<App.Data.DependencyInjectionInstaller>();
            InstallerServices(builder);
        }
        private void InstallerServices(ContainerBuilder builder)
        {
            builder.RegisterType<NoteService>().As<INoteService>();
        }
    }
}
