using EnManaiWebApi.DAO;
using EnManaiWebApi.Model;
using EnManaiWebApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EnManaiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouserOwnerController : ControllerBase
    {
        private readonly ILogger<HouserOwnerController> _logger;
        private readonly IConfiguration _config;
        private string connStr { get; set; }
        private readonly IHouseOwnerDAO _houseownerDAO;
        public HouserOwnerController(ILogger<HouserOwnerController> logger, 
            IConfiguration config,
            IHouseOwnerDAO houseownerDAO)
        {
            _logger = logger;
            _config = config;
            _houseownerDAO = houseownerDAO;
            connStr = _config.GetSection("ConnectionStrings:sql").Value;
        }

        [HttpGet,Route("GetAll")]
        public IActionResult GetAllHouserOwner()
        {
            HouseOwnersResponse resp = new HouseOwnersResponse();
            try
            {
                resp.status = Status.Success;
                List<HouseOwner> hl = _houseownerDAO.GetAll(connStr, _logger); ;
                resp.houseOwners = hl;
                return Ok(resp);
            }
            catch(Exception ex)
            {
                resp.status=Status.DataError;
                resp.ErrorMessage = $"Exception Message:{ex.Message}";
                return Ok(resp);
            }
        }

        [HttpGet, Route("GetByID/{id}")]
        public IActionResult GetByID(int id)
        {
            HouseOwnersResponse resp = new HouseOwnersResponse();
            try
            {
                resp.status = Status.Success;
                List<HouseOwner> h = new List<HouseOwner>() { _houseownerDAO.GetById(id, connStr) };
                resp.houseOwners = h ;
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.status = Status.DataError;
                resp.ErrorMessage = $"Exception Message:{ex.Message}";
                return Ok(resp);
            }
        }

        [HttpPost, Route("Create")]
        public IActionResult Create(HouseOwner houseOwner)
        {
            HouseOwnersResponse resp = new HouseOwnersResponse();
            try
            {
                resp.status = Status.Success;
                //List<HouseOwner> h = new List<HouseOwner>() { _houseownerDAO.GetById(id, connStr) };
                //resp.houseOwners = h;
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.status = Status.DataError;
                resp.ErrorMessage = $"Exception Message:{ex.Message}";
                return Ok(resp);
            }
        }

        [HttpPost, Route("Update")]
        public IActionResult Update(HouseOwner houseOwner)
        {
            HouseOwnersResponse resp = new HouseOwnersResponse();
            try
            {
                resp.status = Status.Success;
                //List<HouseOwner> h = new List<HouseOwner>() { _houseownerDAO.GetById(id, connStr) };
                //resp.houseOwners = h;
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.status = Status.DataError;
                resp.ErrorMessage = $"Exception Message:{ex.Message}";
                return Ok(resp);
            }
        }
    }
}
