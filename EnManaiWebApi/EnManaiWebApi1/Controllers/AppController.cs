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
    public class AppController : ControllerBase
    {
        private ILogger<AppController> _logger;
        private readonly IConfiguration _config;
        private readonly ILoginDAO _loginDAO;
        private readonly IHouseOwnerDAO _houseOwnerDAO;
        private readonly IRentalDetailsDAO _rentalDetailsDAO;
        private readonly ISearchDAO _searchDAO;
        private readonly string connStr;
        private readonly JwtSettings jwtSettings;
        public AppController(ILogger<AppController> logger, ILoginDAO loginDAO, IConfiguration config,
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
        }

        [HttpPost]
        [Route("SearchResult")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Search(string city, string? encryptedUserCode, int id)
        {
            var headers = HttpContext.Request.Headers.Authorization;
            SearchResponse res = new SearchResponse();
            //headers.FirstOrDefault().Split(' ')[1]
            //Login l = this._loginDAO.getUserDetails(id, connStr);
            LoggedSearchResponse logRes = new LoggedSearchResponse();
            List<RentalHouseDetail> resp = _searchDAO.BasicSearch(id,city, connStr);
            Login l = _loginDAO.getUserDetails(id, connStr);
            PaymentForTenant pft =  _loginDAO.GetPayment(id, l.UserName, connStr);
            if ((pft == null ) || (pft.PaymentExpired == true))
            {
                logRes.rentalDetails = ConvertToLoggedRentalHouseDetail(resp);
            }
            res.rentalDetails = resp;
            res.status = Status.Success;
            return Ok(res);
        }

        [HttpPost]
        [Route("NonRegisteredSearchResult")]
        public IActionResult NonRegisteredSearch(string city, string? encryptedUserCode)
        {
            var headers = HttpContext.Request.Headers;
            unregisteredSearchResponse res = new unregisteredSearchResponse();
            var resp = _searchDAO.BasicSearch(0,city, connStr);

            res.rentalDetails = ConvertTounregisteredRentalHouseDetail(resp);
            res.status = Status.Success;
            return Ok(res);
        }
        #region Common
        private List<UnregisteredRentalHouseDetail> ConvertTounregisteredRentalHouseDetail(List<RentalHouseDetail> rentalDetails)
        {
            List<UnregisteredRentalHouseDetail> unregisteredRentalHouseDetails = new List<UnregisteredRentalHouseDetail>();
            foreach (var item in rentalDetails)
            {
                UnregisteredRentalHouseDetail urRentalDetails = new UnregisteredRentalHouseDetail()
                {
                    HouseOwnerId = item.HouseOwnerId,
                    City = item.City,
                    Bachelor = item.Bachelor,
                    CoOperationWater = item.CoOperationWater ,
                    Id = item.Id ,
                    NonVeg = item.NonVeg ,
                    Pincode = item.Pincode 
                };
                unregisteredRentalHouseDetails.Add(urRentalDetails);
            }
            return unregisteredRentalHouseDetails;

        }

        private List<LoggedRentalHouseDetail> ConvertToLoggedRentalHouseDetail(List<RentalHouseDetail> rentalDetails)
        {
            List<LoggedRentalHouseDetail> loggedRentalHouseDetail = new List<LoggedRentalHouseDetail>();
            foreach (var item in rentalDetails)
            {
                LoggedRentalHouseDetail loggedRentalDetails = new LoggedRentalHouseDetail()
                {
                    Id = item.Id,
                    HouseOwnerId = item.HouseOwnerId,
                    City = item.City,
                    Pincode = item.Pincode,
                    CoOperationWater = item.CoOperationWater,
                    TwoWheelerParking = item.TwoWheelerParking,
                    FourWheelerParking = item.FourWheelerParking,
                    SeparateHouse = item.SeparateHouse,
                    PetsAllowed = item.PetsAllowed,
                    RentFrom = item.RentFrom,
                    BHK = item.BHK,
                    Bachelor = item.Bachelor,
                    NonVeg = item.NonVeg,
                };
                loggedRentalHouseDetail.Add(loggedRentalDetails);
            }
            return loggedRentalHouseDetail;

        }
        #endregion
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
            
            Login l = _loginDAO.GetLogin(0, loginRequest.UserName,loginRequest.Password, connStr);
            res.paymentForTenant = _loginDAO.GetPayment(l.ID, l.UserName, connStr);
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
            Login l = _loginDAO.GetLogin(0, username,"", connStr);
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
