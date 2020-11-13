using Microsoft.AspNetCore.Mvc;
using Sales.Application.Interfaces;
using System.Threading.Tasks;

namespace Sales.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public PersonsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// SELECT records from mock data GenFu
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Persons.GetAllAsync();
            return Ok(data);
        }
    }
}