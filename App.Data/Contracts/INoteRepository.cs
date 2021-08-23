using App.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Contracts
{
    public interface INoteRepository : IRepository<Note>
    {
        bool IsDuplicateNote(int agentId, string name);
    }
}
