using Sales.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IProductRepository productRepository)
        {
            Products = productRepository;
        }
        public IProductRepository Products { get; }
    }
}
