using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
    }
}
