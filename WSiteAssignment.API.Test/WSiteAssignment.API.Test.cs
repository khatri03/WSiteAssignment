using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSiteAssignment.API.Controllers;
using WSiteAssignment.Data;
using WSiteAssignment.Models.DbEntities;
using WSiteAssignment.Repository.UoW;

namespace WSiteAssignment.API.Test
{
    [TestClass]
    public class WSiteAssignmentApiTest1
    {
        private DataContext _context;
        private IUnitOfWork _UoW;
        private WriteController writeController;

        [TestInitialize]
        public void Initialize()
        {
            this._context = DataContextMocker.GetDataContext("WSiteAssign_UnitTest");
            this._UoW = new UnitOfWork(this._context);
            this.writeController = new WriteController(this._UoW);
        }

        [TestMethod]
        public async Task Test_WriteController_InsertSuccess()
        {
            // Arrange
            Employee emp = new Employee()
            {
                Email = "test@test.com",
                FirstName = "Test First",
                Id = 0,
                LastName = "Test Last"
            };
            emp.Id = this._UoW.Employees.GetAll().ToList()
                .Max(e => e.Id) + 1;

            //Act
            IActionResult response = await writeController.Insert(emp);
            StatusCodeResult responseCode = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(responseCode.StatusCode, (int)System.Net.HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Test_WriteController_InsertInvalidModel()
        {
            // Arrange
            Employee emp = new Employee()
            {
                Email = "test@test.com",
                FirstName = "Test First",
                Id = 0,
                LastName = null
            };
            emp.Id = this._UoW.Employees.GetAll().ToList()
                .Max(e => e.Id) + 1;
            writeController.ModelState.AddModelError("LastName", "Last name is required.");

            //Act
            IActionResult response = await writeController.Insert(emp);
            StatusCodeResult responseCode = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(responseCode.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void Test_WriteController_DeleteByObjectSuccess()
        {
            // Arrange
            Employee emp = new Employee()
            {
                Email = "test@test.com",
                FirstName = "Test First",
                Id = 1,
                LastName = "Test Last"
            };

            //Act
            var response = writeController.Delete(emp);

            //Assert
            Assert.AreEqual(response, true);
        }

        [TestMethod]
        public void Test_WriteController_DeleteByObjectFail()
        {
            // Arrange
            Employee emp = new Employee()
            {
                Email = "test@test.com",
                FirstName = "Test First",
                Id = 0,
                LastName = "Test Last"
            };

            //Act
            var response = writeController.Delete(emp);

            //Assert
            Assert.AreEqual(response, false);
        }

        [TestMethod]
        public void Test_WriteController_DeleteByIdSuccess()
        {
            // Arrange
            WriteController writeController = new WriteController(this._UoW);

            //Act
            var response = writeController.Delete(4);

            //Assert
            Assert.AreEqual(response, true);
        }

        [TestMethod]
        public void Test_WriteController_DeleteByIdFail()
        {
            // Arrange
            WriteController writeController = new WriteController(this._UoW);

            //Act
            var response = writeController.Delete(-1);

            //Assert
            Assert.AreEqual(response, false);
        }

        [TestMethod]
        public void Test_WriteController_UpdateSuccess()
        {
            // Arrange
            Employee emp = new Employee()
            {
                Email = "test@test.com",
                FirstName = "Test First",
                Id = 2,
                LastName = "Test Last"
            };

            //Act
            var response = writeController.Update(emp);

            //Assert
            Assert.AreEqual(response, true);
        }

        [TestMethod]
        public void Test_WriteController_UpdateFail()
        {
            // Arrange
            Employee emp = new Employee()
            {
                Email = "test@test.com",
                FirstName = "Test First",
                Id = 12,
                LastName = "Test Last"
            };

            emp.Id = this._UoW.Employees.GetAll().ToList()
                .Max(e => e.Id) + 1;

            //Act
            var response = writeController.Update(emp);

            //Assert
            Assert.AreEqual(response, false);
        }

        [TestMethod]
        public async Task Test_ReadControllerGetByIdSuccess()
        {
            //Arrange
            ReadController readController = new ReadController(this._UoW);

            //Act
            var results = await readController.Get(5) as ObjectResult;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsTrue(results.Value is Employee);
        }

        [TestMethod]
        public async Task Test_ReadControllerGetByIdFailed()
        {
            //Arrange
            ReadController readController = new ReadController(this._UoW);

            //Act
            var results = await readController.Get(-1) as  StatusCodeResult;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        public async Task Test_ReadControllerGetAllSuccess()
        {
            //Arrange
            ReadController readController = new ReadController(this._UoW);

            //Act
            var results = await readController.GetAll() as ObjectResult;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(results.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsTrue(results.Value is IEnumerable<Employee>);
        }
    }
}
