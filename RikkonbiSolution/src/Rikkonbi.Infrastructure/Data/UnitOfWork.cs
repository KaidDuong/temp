using Rikkonbi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private RikkonbiDbContext _dbContext;

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}