using EnManaiWebApi.DAO;
using EnManaiWebApi.Model;
using EnManaiWebApi.Model.Request;
using EnManaiWebApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnManaiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly ILoginDAO _loginDAO;
        private readonly string connStr;
        public HomeController(ILogger<HomeController> logger,ILoginDAO loginDAO,IConfiguration config)
        {
            _logger = logger;
            _loginDAO = loginDAO;
            _config = config;
            connStr = _config.GetSection("ConnectionStrings:sql").Value;
        }
        #region Rental Detail

        [HttpGet]
        [Route("GetAllRentalDetails")]
        public RentalDetailsResponse GetAllRentalDetails()
        {
            _logger.LogInformation("Get All Rentail Details");
            RentalDetailsResponse res = new RentalDetailsResponse();
            return res;
        }

        [HttpGet]
        [Route("GetRentalDetail/{id}")]
        public IActionResult GetRentalDetail(int id)
        {
            RentalDetailsResponse res = new RentalDetailsResponse();
            return Ok(res);
        }
        [HttpPost]
        [Route("CreateRentalDetail")]
        public IActionResult CreateRentalDetail([FromBody]HouseOwner houseOwner)
        {
            Response res = new Response();
            res.status = Status.Success;
            return Ok(res);
        }

        [HttpPost]
        [Route("UpdateRentalDetail")]
        public IActionResult UpdateRentalDetail([FromBody] RentalDetails RentalDetail)
        {
            Response res = new Response();
            res.status = Status.Success;
            return Ok(res);
        }

        [HttpPost]
        [Route("DeleteRentalDetail/{id}")]
        public IActionResult DeleteRentalDetail(int id)
        {
            Response res = new Response();
            res.status = Status.Success;
            return Ok(res);
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("GetAllLogin")]
        public IActionResult GetAllLogin()
        {
            LoginResponse res = new LoginResponse();
            res.logins = _loginDAO.GetAll(connStr) ;
            res.status = Status.Success;
            return Ok(res);
        }
        [HttpPost]
        [Route("GetLogin")]
        public IActionResult GetLogin(LoginRequest loginRequest)
        {
            LoginResponse res = new LoginResponse();
            res.logins = new List<Login>();
            Login l = _loginDAO.GetLogin(0, loginRequest.UserName, connStr);
            res.logins.Add(l);
            res.status = Status.Success;
            return Ok(res);
        }
        [HttpPost]
        [Route("UpdateLogin")]
        public IActionResult UpdateLogin(string username)
        {
            LoginResponse res = new LoginResponse();
            res.logins = new List<Login>();
            Login l = _loginDAO.GetLogin(0, username, connStr);
            res.logins.Add(l);
            res.status = Status.Success;
            return Ok(res);
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult CreateLogin(Login login)
        {
            LoginResponse res = new LoginResponse();
            try
            {
                _loginDAO.InsertLogin(login, connStr);
                res.logins = new List<Login>();
                res.status = Status.Success;
                return Ok(res);
            }catch(Exception ex)
            {
                _logger.LogInformation($"Exception :{ex.Message}");
                res.status = Status.DataError;
                res.ErrorMessage = ex.Message;
                return Ok(res);
            }
            
        }

        [HttpPost]
        [Route("GetLoginHouseDetails")]
        public IActionResult GetLoginHouseDetails()
        {

            return null;
        }
        #endregion
    }
}
