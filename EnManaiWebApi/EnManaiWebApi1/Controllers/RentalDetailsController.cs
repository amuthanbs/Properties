using EnManaiWebApi.DAO;
using EnManaiWebApi.Model;
using EnManaiWebApi.Model.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnManaiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalDetailsController : ControllerBase
    {
        private ILogger<AppController> _logger;
        private readonly IConfiguration _config;
        private readonly IRentalHouseDetails _rhd;
        private string connStr;
        // GET: api/<RentalDetailsController>
        public RentalDetailsController(ILogger<AppController> logger, IConfiguration config, IRentalHouseDetails rhd)
        {
            _logger = logger;
            _config = config;
            _rhd = rhd;
            connStr = _config.GetSection("ConnectionStrings:sql").Value;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RentalDetailsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RhdPhoneNumberResponse response = null;
            try
            {
                RentalHouseDetailsDAO rentalHouseDetailsDAO = new RentalHouseDetailsDAO();
                
                RentalHouseDetailPhoneNumber rhdPhoneNumber = rentalHouseDetailsDAO.GetPhoneNumberById(id, connStr);
                response.status = Status.Success;
                response.RentalHouseDetailPhoneNumber = rhdPhoneNumber;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.status = Status.DataError;
                response.ErrorMessage = ex.Message;
            }
            return Ok(response);
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
