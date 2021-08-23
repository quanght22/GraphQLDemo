using App.GraphQL.Contracts;
using App.GraphQL.RootTypes.Queries;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.GraphQL
{
    public class GraphSchema : Schema
    {
        public GraphSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<RootQuery>();
        }
    }
}