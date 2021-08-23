using App.Data.Contracts;
using App.Infrastructure;
using App.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Data.Repositories
{
    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public bool IsDuplicateNote(int noteId, string title)
        {
            return _dbContext.Set<Note>().Any(x => x.Id != noteId && x.Title == title);
        }
        public override Note GetById(int id)
        {
            return _dbContext.Set<Note>().FirstOrDefault(x => x.Id == id);
        }
    }
}
