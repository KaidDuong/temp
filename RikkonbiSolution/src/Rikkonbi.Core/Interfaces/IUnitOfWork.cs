using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Core.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}