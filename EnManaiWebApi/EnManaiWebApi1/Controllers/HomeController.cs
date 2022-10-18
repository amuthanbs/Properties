using EnManaiWebApi.DAO;
using EnManaiWebApi.Model;
using EnManaiWebApi.Model.Request;
using EnManaiWebApi.Model.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TokenBased.Extension;
using TokenBased.JwtHHelpers;
using TokenBased.Model;
using TokenBased.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace EnManaiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly ILoginDAO _loginDAO;
        private readonly IHouseOwnerDAO _houseOwnerDAO;
        private readonly IRentalDetailsDAO _rentalDetailsDAO;
        private readonly ISearchDAO _searchDAO;
        private readonly string connStr;
        private readonly JwtSettings jwtSettings;
        public HomeController(ILogger<HomeController> logger, ILoginDAO loginDAO, IConfiguration config,
            IHouseOwnerDAO houseOwnerDAO,
            IRentalDetailsDAO rentalDetailsDAO,
            ISearchDAO searchDAO, JwtSettings jwtSettings)
        {
            _logger = logger;
            _loginDAO = loginDAO;
            _config = config;
            _houseOwnerDAO = houseOwnerDAO;
            _rentalDetailsDAO = rentalDetailsDAO;
            _searchDAO = searchDAO;
            connStr = _config.GetSection("ConnectionStrings:sql").Value;
            this.jwtSettings = jwtSettings;
        }
        #region Rental Detail

        [HttpGet]
        [Route("GetAllRentalDetails")]
        public RentalDetailsResponse GetAllRentalDetails()
        {
            _logger.LogInformation("Get All Rentail Details");
            RentalDetailsResponse res = new RentalDetailsResponse();
            RentalDetails rental = new RentalDetails();
            rental.houseOwner = _houseOwnerDAO.GetById(1, connStr);
            return res;
            return res;
        }

        [HttpPost]
        [Route("SearchResult")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Search(string city, string? encryptedUserCode)
        {
            SearchResponse res = new SearchResponse();
            var resp = _searchDAO.BasicSearch(city, connStr);
            res.rentalDetails = resp;
            res.status = Status.Success;
            return Ok(res);
        }


        [HttpPost]
        [Route("GetRentalDetail")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult RentalDetails(int id)
        {
            RentalDetailsResponse res = new RentalDetailsResponse();
            try
            {

                res.rentalDetails = new RentalDetails();
                RentalDetails rental = new RentalDetails();
                rental.houseOwner = _houseOwnerDAO.GetById(1, connStr);
                if (rental.houseOwner != null)
                {
                    rental.rentalHouseDetails = _rentalDetailsDAO.GetOwnerRentalHouse(rental.houseOwner.Id, connStr);
                }
                res.rentalDetails = rental;
                res.status = Status.Success;
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
                res.status = Status.DataError;
                return Ok(res);
            }
        }
        [HttpPost]
        [Route("CreateRentalDetail")]
        public IActionResult CreateRentalDetail([FromBody] HouseOwner houseOwner)
        {
            Response res = new Response();
            res.status = Status.Success;
            return Ok(res);
        }

        [HttpPost]
        [Route("UpdateRentalDetail")]
        public IActionResult UpdateRentalDetail(RentalHouseDetail rentalHouseDetails)
        {
            UpdateResponse res = new UpdateResponse();
            try
            {
                int cnt = _rentalDetailsDAO.Save(rentalHouseDetails, connStr);
                res.Count = cnt;
                if (cnt > 0)
                {
                    res.status = Status.Success;
                }
                else
                {
                    res.status = Status.DataError;
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"UpdateRentalDetils Exception: Messsage:{ex.Message} InnerException:{ex.InnerException}");
                res.status = Status.InternalError;
                res.ErrorMessage = "Update Rental Details is unsuccessfull";
                return Ok(res);
            }

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


        [HttpGet]
        [Route("GetAllLogin")]

        public IActionResult GetAllLogin()
        {
            LoginResponse res = new LoginResponse();
            res.logins = _loginDAO.GetAll(connStr);
            res.status = Status.Success;
            return Ok(res);
        }
        [HttpPost]
        [Route("GetLogin")]
        //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetLogin(LoginRequest loginRequest)
        {
            var Valid = false;
            var Token = new UserTokens();
            LoginResponse res = new LoginResponse();
            res.logins = new List<Login>();
            Login l = _loginDAO.GetLogin(0, loginRequest.UserName, connStr);
            if (l == null)
            {
                res.status = Status.DataError;
                res.ErrorMessage = "User doesn't exists";
                return Ok(res);
            }
            else
            {
                Valid = true;
            }
            res.logins.Add(l);
            
            res.status = Status.Success;
            
                           //var Token = new UserTokens();
                           //var Valid = logins.Any(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                Token = TokenBased.Extension.JwtHelpers.GenTokenkey(new UserTokens()

                {
                    EmailId = l.EMailId,
                    GuidId = Guid.NewGuid(),
                    UserName = l.UserName,
                    Id = l.ID,
                }, jwtSettings);
                //var user = logins.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                //Token = JwtHelpers.GenTokenkey(new UserTokens()

                //    {
                //        EmailId = l.EMailId,
                //        GuidId = Guid.NewGuid(),
                //        UserName = l.UserName,
                //        Id = l.ID,
                //    }, jwtSettings);
                }
                else
                {
                    return BadRequest($"wrong password");
                }
                res.accessToken = Token;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
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
            }
            catch (Exception ex)
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
