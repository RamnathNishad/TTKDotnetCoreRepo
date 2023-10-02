using EFCoreCRUDLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmpDataAccess _dal;
        public EmployeesController(IEmpDataAccess dal)
        {
            this._dal= dal;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var lstEmps = _dal.GetEmps();
            return lstEmps;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return _dal.GetEmpById(id);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] Employee emp)
        {
            _dal.AddEmployee(emp);
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee emp)
        {
            _dal.UpdateEmployee(emp);
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dal.DeleteEmployee(id);
        }

        [HttpGet]
        [Route("SearchEmployee/{id}")]
        public IActionResult SearchEmployee(int id)
        {
            try
            {
                var emp = _dal.GetEmpById(id);
                if (emp == null)
                {
                    return NotFound("Employee not found");
                }
                else
                {
                    return Ok(emp);
                }
            }
            catch(Exception ex)
            {
                //log this server error in logfile using logger                
                return BadRequest(ex.Message);
            }
        }
    }
}
