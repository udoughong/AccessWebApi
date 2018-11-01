using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi4SimpleSource.Controllers
{
    public class EmployeesController : ApiController
    {
        static List<string> employees = new
        List<string> { "Mary", "Candy", "Lilly", "Betty", "Jessica" };
        public IEnumerable<string> Get()
        {
            return employees;
        }
        public string Get(int id)
        {
            try
            {
                return employees[id];
            }
            catch (Exception)
            {
                return "none";
            }
        }
        public void Post([FromBody]string value)
        {
            employees.Add(value);
        }
        public void Put(int id, [FromBody]string value)
        {
            try
            {
                employees[id] = value;
            }
            catch (Exception)
            {
            }
        }
        public void Delete(int id)
        {
            try
            {
                employees.RemoveAt(id);
            }
            catch (Exception)
            {
            }
        }
    }

}
