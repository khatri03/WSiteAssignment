using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSiteAssignment.Models.DbEntities;
using WSiteAssignment.Repository.UoW;

namespace WSiteAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public ReadController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [Route("get/{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var emp = await this._unitOfWork.Employees.GetAsync(Id);
            if(emp != null)
            {
                return StatusCode((int)System.Net.HttpStatusCode.OK, emp);
            }
            return StatusCode((int)System.Net.HttpStatusCode.NotFound);
        }

        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var results = await this._unitOfWork.Employees.GetAllAsync();
            if(results != null && results.ToList().Count > 0)
            {
                return StatusCode((int)System.Net.HttpStatusCode.OK, results);
            }
            return StatusCode((int)System.Net.HttpStatusCode.NotFound);
        }
    }
}