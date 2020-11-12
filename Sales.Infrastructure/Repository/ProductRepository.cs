using Dapper;
using Sales.Application.Interfaces;
using Sales.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SqlKata.Execution;

namespace Sales.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration configuration;
        private readonly QueryFactory _db;

        public ProductRepository(IConfiguration configuration, QueryFactory db)
        {
            this.configuration = configuration;
            _db = db;

        }
        public async Task<Guid> AddAsync(Product entity)
        {
            entity.AddedOn = DateTime.Now;
            entity.Id = Guid.NewGuid();
            //var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            //using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            //{
            //    connection.Open();
            //    var result = await connection.ExecuteAsync(sql, entity);
            //    return c;
            //}

            var affectedRecords = await _db.Query("Products").InsertAsync(new
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Barcode = entity.Barcode,
                Rate = entity.Rate,
                AddedOn = entity.AddedOn
            });

            return entity.Id;


        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //var sql = "SELECT * FROM Products";
            //using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            //{
            //    connection.Open();
            //    var result = await connection.QueryAsync<Product>(sql);
            //    return result.ToList();
            //}

            var result = _db.Query("Products")
                .Select(
                "Id",
                "Name",
                "Barcode",
                "Description",
                "Rate",
                "AddedOn",
                "ModifiedOn");

            return await result.GetAsync<Product>();


        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
