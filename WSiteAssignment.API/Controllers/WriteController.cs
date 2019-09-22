using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSiteAssignment.Models.DbEntities;
using WSiteAssignment.Repository.UoW;

namespace WSiteAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriteController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public WriteController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [Route("delete/{Id:int}")]
        [HttpDelete]
        public bool Delete(int Id)
        {
            var emp = this._unitOfWork.Employees.FindBy(e => e.Id == Id).FirstOrDefault();
            if(emp != null)
            {
                this._unitOfWork.Employees.Remove(emp);
            }
            return this._unitOfWork.SaveAll();
        }

        [Route("delete")]
        [HttpDelete]
        public bool Delete(Employee emp)
        {
            if (emp != null)
            {
                var existing = this._unitOfWork.Employees.FindBy(e => e.Id == emp.Id).FirstOrDefault();
                if(existing != null)
                {
                    this._unitOfWork.Employees.Remove(existing);
                }
            }
            return this._unitOfWork.SaveAll();
        }

        [Route("update")]
        [HttpPut]
        public bool Update(Employee emp)
        {
            if (emp != null)
            {
                var existing = this._unitOfWork.Employees.FindBy(e => e.Id == emp.Id).FirstOrDefault();
                if (existing != null)
                {
                    this._unitOfWork.Employees.Update(emp);
                }
            }
            return this._unitOfWork.SaveAll();
        }

        [Route("insert")]
        [HttpPost]
        public async Task<IActionResult> Insert(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._unitOfWork.Employees.Add(emp);
                    await this._unitOfWork.SaveAllAsync();
                    return StatusCode((int)System.Net.HttpStatusCode.Created);
                }
                else
                {
                    return StatusCode((int)System.Net.HttpStatusCode.BadRequest);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
    }
}