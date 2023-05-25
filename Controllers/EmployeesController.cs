using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;

namespace MongoExample.Controllers; 

[Controller]
[Route("api/[controller]")]

public class EmployeesController: Controller {
   
   private readonly MongoDBService _mongoDBService;

    public EmployeesController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
        public ActionResult<List<Employees>> Get() =>
            _mongoDBService.Get();

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        public ActionResult<Employees> Get(string id)
        {
            var employee = _mongoDBService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public ActionResult<Employees> Create(Employees employee)
        {
            _mongoDBService.Create(employee);

            return CreatedAtRoute("GetEmployee", new { id = employee.Id.ToString() }, employee);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Employees employeeIn)
        {
            var employee = _mongoDBService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            _mongoDBService.Update(id, employeeIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var employee = _mongoDBService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            _mongoDBService.Delete(employee.Id);

            return NoContent();
        }
}