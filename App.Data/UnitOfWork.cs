using App.Data.Contracts;
using App.Infrastructure;
using System;

namespace App.Data
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        internal ApplicationDbContext dbContext;
        public INoteRepository NoteRepository { get; set; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SaveChanges()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
