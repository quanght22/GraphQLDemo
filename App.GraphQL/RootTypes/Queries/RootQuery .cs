using App.GraphQL.Contracts;
using App.Models.Settings;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.GraphQL.RootTypes.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<UserServiceQuery>(typeof(ServiceSetting.UserService).Name.ToLower(), resolve: context => new { });
            Field<NoteServiceQuery>(typeof(ServiceSetting.NoteService).Name.ToLower(), resolve: context => new { });
        }
    }
   
}
