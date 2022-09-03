using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnManaiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalDetailsController : ControllerBase
    {
        // GET: api/<RentalDetailsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RentalDetailsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RentalDetailsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RentalDetailsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RentalDetailsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
