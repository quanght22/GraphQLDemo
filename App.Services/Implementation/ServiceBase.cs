using App.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Implementation
{
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
