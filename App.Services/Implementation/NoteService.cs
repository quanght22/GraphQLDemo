using App.Data.Contracts;
using App.Models.DbEntities;
using App.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Implementation
{
    public class NoteService : ServiceBase, INoteService
    {

        public NoteService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Note GetNode(int Id)
        {
            try
            {
                var note = this._unitOfWork.NoteRepository.GetById(1);
                return note;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
