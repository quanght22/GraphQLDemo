using App.GraphQL.RootTypes.Queries;
using App.GraphQL.Types;
using App.Infrastructure;
using Autofac;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.GraphQL
{

    public class DependencyInjectionInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<App.Services.DependencyInjectionInstaller>();
            InstallerGraphQLServices(builder);
        }
        private void InstallerGraphQLServices(ContainerBuilder builder)
        {
            builder.RegisterType<NoteType>();
            builder.RegisterType<NoteServiceQuery>();
            builder.RegisterType<UserServiceQuery>();
            builder.RegisterType<RootQuery>().AsSelf().SingleInstance();
            builder.RegisterType<GraphSchema>().AsSelf().SingleInstance();
        }
    }
}
