using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        INoteRepository NoteRepository { get; set; }
    }
}