using App.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Contracts
{
    public interface INoteService
    {
        Note GetNode(int Id);
    }
}
