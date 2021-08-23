using App.GraphQL.Contracts;
using App.GraphQL.Types;
using App.Models.Settings;
using App.Services.Contracts;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.GraphQL.RootTypes.Queries
{
    public class UserServiceQuery : ObjectGraphType
    {
        public UserServiceQuery(INoteService noteService)
        {

            Field<NoteType>(
                ServiceSetting.UserService.GetUserById.ToLower(),
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    int noteId = context.GetArgument<int>("id");
                    return noteService.GetNode(noteId);
                });
        }
    }
}
