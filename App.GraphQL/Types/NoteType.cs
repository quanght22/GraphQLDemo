using App.Models.DbEntities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.GraphQL.Types
{
    public class NoteType : ObjectGraphType<Note>
    {
        public NoteType()
        {
            Field(x => x.Id);
            Field(x => x.Title);
        }
    }
}
