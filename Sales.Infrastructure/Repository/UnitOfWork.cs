using Sales.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository productRepository, IPersonRepository personRepository)
        {
            Products = productRepository;
            Persons = personRepository;
        }
        public IProductRepository Products { get; }

        public IPersonRepository Persons { get; }


    }
}
