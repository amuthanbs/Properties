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
using MessagePack;

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
        private readonly ISMSVerificationDAO _iSMSVerificationDAO;
        private readonly string connStr;
        private readonly JwtSettings jwtSettings;
        int SMSReverificationPeriodInDays = 0;
        int RecordsPerDay = 0;
        int Start = 0;
        public AppController(ILogger<AppController> logger, ILoginDAO loginDAO, IConfiguration config,
            IHouseOwnerDAO houseOwnerDAO,
            IRentalDetailsDAO rentalDetailsDAO,
            ISearchDAO searchDAO, JwtSettings jwtSettings,
            ISMSVerificationDAO iSMSVerificationDAO)
        {
            _logger = logger;
            _loginDAO = loginDAO;
            _config = config;
            _houseOwnerDAO = houseOwnerDAO;
            _rentalDetailsDAO = rentalDetailsDAO;
            _searchDAO = searchDAO;
            _iSMSVerificationDAO = iSMSVerificationDAO;
            connStr = _config.GetSection("ConnectionStrings:sql").Value;
            SMSReverificationPeriodInDays = Int32.Parse(_config.GetValue<string>("SMSReverificationPeriodInDays"));
            RecordsPerDay = Int32.Parse(_config.GetValue<string>("RecordsPerPage"));
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
        public IActionResult Search(string city, string? encryptedUserCode, int id, int start = 0, int end = 5)
        {
            Response res = new Response();
            try
            {
                var headers = HttpContext.Request.Headers.Authorization;
                
                PaidedSearchResponse paidRes = new PaidedSearchResponse();
                //headers.FirstOrDefault().Split(' ')[1]
                //Login l = this._loginDAO.getUserDetails(id, connStr);
                LoggedSearchResponse logRes = new LoggedSearchResponse();
                List<RentalHouseDetail> resp = _searchDAO.BasicSearch(id, city, connStr, start, end);
                Login l = _loginDAO.getUserDetails(id, connStr);

                if (l.Paided == true)
                {
                    PaymentForTenant pft = _loginDAO.GetPayment(id, l.UserName, connStr);
                    if (pft != null ) // Tenant Paid done for  at list 1 time 
                    {
                        if (pft.PaymentExpired == false)
                        {
                            paidRes.rentalDetails = ConvertToPaidedTenant(resp);
                            paidRes.status = Status.Success;
                            paidRes.paidUser = true;
                            paidRes.paidExpiredUser = false;
                            paidRes.Logged = false;
                            return Ok(paidRes);
                        }
                    }
                    else if ((pft == null) || (pft.PaymentExpired == true))
                    {
                        logRes.rentalDetails = ConvertToLoggedTenant(resp);
                        logRes.status = Status.Success;
                        logRes.paidExpiredUser = true;
                        logRes.paidUser = false;
                        logRes.Logged = false;
                        return Ok(logRes);
                    }
                    //else
                    //{
                    //    return Ok(res);
                    //}
                }
                else // Tenant logged not paid any payment
                {
                    if (l.NonPaidedContactViewed <= l.NoOfNonPaidedContact)
                    {
                        paidRes.rentalDetails = ConvertToPaidedTenant(resp);
                        paidRes.status = Status.Success;
                        paidRes.paidExpiredUser = false;
                        paidRes.paidUser = false;
                        paidRes.Logged = true;
                        return Ok(paidRes);
                    }
                    else
                    {
                        logRes.rentalDetails = ConvertToLoggedTenant(resp);
                        logRes.status = Status.Success;
                        logRes.paidExpiredUser = false;
                        logRes.paidUser = false;
                        logRes.Logged = false;
                        return Ok(logRes);
                    }
                }
                res.status = Status.DataError;
                res.ErrorMessage = "No Result for Logged User";
                //res.rentalDetails = resp;
                //res.status = Status.Success;
                return Ok(res);
            }catch (Exception ex)
            {
                res.status = Status.InternalError;
                string innerMessage = ex.InnerException == null ? "" : ex.InnerException.Message;
                res.ErrorMessage = $"Exception Message:{ex.Message} Inner Exception:{innerMessage}";
                return Ok(res);
            }
        }

        [HttpPost]
        [Route("NonRegisteredSearchResult")]
        public IActionResult NonRegisteredSearch(string city, string? encryptedUserCode)
        {
            var headers = HttpContext.Request.Headers;
            unregisteredSearchResponse res = new unregisteredSearchResponse();
            var resp = _searchDAO.BasicSearch(0, city, connStr, Start, RecordsPerDay);

            res.rentalDetails = ConvertTounregisteredRentalHouseDetail(resp);
            res.status = Status.Success;
            return Ok(res);
        }
        #region Common

        public static Object Copy(Object parent, Object child)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
            return child;
        }

        private List<UnregisteredRentalHouseDetail> ConvertTounregisteredRentalHouseDetail(List<RentalHouseDetail> rentalDetails)
        {
            List<UnregisteredRentalHouseDetail> unregisteredRentalHouseDetails = new List<UnregisteredRentalHouseDetail>();
            foreach (var item in rentalDetails)
            {
                UnregisteredRentalHouseDetail urRentalDetails = new UnregisteredRentalHouseDetail();
                //UnregisteredRentalHouseDetail urRentalDetails = new UnregisteredRentalHouseDetail()
                //{
                //    HouseOwnerId = item.HouseOwnerId,
                //    City = item.City,
                //    Bachelor = item.Bachelor,
                //    CoOperationWater = item.CoOperationWater,
                //    Id = item.Id,
                //    NonVeg = item.NonVeg,
                //    Pincode = item.Pincode
                //};
                UnregisteredRentalHouseDetail ur = (UnregisteredRentalHouseDetail)Copy(item, urRentalDetails);
                unregisteredRentalHouseDetails.Add(ur);
            }
            return unregisteredRentalHouseDetails;

        }

        //private List<LoggedRentalHouseDetail> ConvertToLoggedRentalHouseDetail(List<RentalHouseDetail> rentalDetails)
        private List<LoggedRentalHouseDetail> ConvertToLoggedTenant(List<RentalHouseDetail> rentalDetails)
        {
            List<LoggedRentalHouseDetail> loggedRentalHouseDetail = new List<LoggedRentalHouseDetail>();
            foreach (var item in rentalDetails)
            {
                if (item.PaymentActive)
                {
                    LoggedRentalHouseDetail loggedRentalDetails = new LoggedRentalHouseDetail()
                    {
                        Id = item.Id,
                        HouseOwnerId = item.HouseOwnerId,
                        Deposit = item.Deposit,
                        AreaOrNagar = item.AreaOrNagar,
                        Floor = item.Floor,
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
                        //PhoneNumber = item.PhoneNumber,
                        //PhoneNumberprimary = item.PhoneNumberPrimary,
                        //LandLineNumber = item.LandLineNumber
                    };
                    loggedRentalHouseDetail.Add(loggedRentalDetails);
                }
            }
            return loggedRentalHouseDetail;

        }

        //private List<LoggedRentalHouseDetail> ConvertToPaidedTenantPaymentExpired(List<RentalHouseDetail> rentalDetails)
        private List<PaidedRentalHouseDetail> ConvertToPaidedTenant(List<RentalHouseDetail> rentalDetails)
        {
            List<PaidedRentalHouseDetail> loggedRentalHouseDetail = new List<PaidedRentalHouseDetail>();
            foreach (var item in rentalDetails)
            {
                //if (item.PaymentActive)
                //{
                    PaidedRentalHouseDetail loggedRentalDetails = new PaidedRentalHouseDetail()
                    {
                        Id = item.Id,
                        HouseOwnerId = item.HouseOwnerId,
                        AreaOrNagar = item.AreaOrNagar,
                        Floor = item.Floor,
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
                        FlatNoOrDoorNo = item.FlatNoOrDoorNo,
                        Address1 = item.Address1,
                        Address2 = item.Address2,
                        Address3 = item.Address3,
                        District = item.District,
                        State = item.State,
                        Vasuthu = item.Vasuthu,
                        BoreWater = item.BoreWater,
                        SeparateEB = item.SeparateEB,
                        HouseOwnerResidingInSameBuilding = item.HouseOwnerResidingInSameBuilding,
                        RentalOccupied = item.RentalOccupied,
                        Apartment = item.Apartment,
                        ApartmentFloor = item.ApartmentFloor,
                        PaymentActive = item.PaymentActive,
                    };
                    loggedRentalHouseDetail.Add(loggedRentalDetails);
                //}
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
        [HttpPost]
        [Route("GetPhoneNumber")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetPhoneNumber(int id, int rentalid)
        {
            RhdPhoneNumberResponse response = new RhdPhoneNumberResponse();
            try
            {
                Login login = _loginDAO.getUserDetails(id, connStr);
                string cntList = login.NonPaidContactList;
                RentalHouseDetail rhd = rhd = _rentalDetailsDAO.GetById(rentalid, connStr);
                response.RentalHouseDetailPhoneNumber = new RentalHouseDetailPhoneNumber()
                {
                    Id = id,
                    LandLineNumber = rhd.LandLineNumber,
                    PhoneNumberPrimary = rhd.PhoneNumberPrimary,
                    PhoneNumber = rhd.PhoneNumber,
                };
                response.status = Status.Success;
                string updateNonPaidContactList = (cntList + ',' + rentalid.ToString()).Trim(',');
                if (String.IsNullOrEmpty(cntList) || (!cntList.Split(',').Contains(rentalid.ToString())))
                {
                   response.RentalHouseDetailPhoneNumber.login =  _loginDAO.UpdateNonPaidContactList(id, updateNonPaidContactList, connStr);
                }else
                {
                    response.RentalHouseDetailPhoneNumber.login = login;
                }
            }catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.status = Status.Success;
            }
            return Ok(response);
        }
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

            Login l = _loginDAO.GetLogin(0, loginRequest.UserName, loginRequest.Password, connStr);

            if (l == null)
            {
                res.status = Status.DataError;
                res.ErrorMessage = "User doesn't exists";
                return Ok(res);
            }
            else
            {
                Valid = true;
                //DateTime dt = ((DateTime) l.PhoneNumberVerifiedDate).AddDays(SMSReverificationPeriodInDays);
                //double days = ( DateTime.Now - (DateTime)l.PhoneNumberVerifiedDate).TotalDays;
                //if( days >= SMSReverificationPeriodInDays)
                //{
                //    l.PhoneNumberVerified = false;
                //    l.ReVerification = true;
                //    try
                //    {
                //        _loginDAO.updatePhoneVerificationStatus(l, connStr);
                //    } catch (Exception ex)
                //    {
                //        throw Exception("SMS Verification date")
                //    }
                //}
            }
            res.paymentForTenant = _loginDAO.GetPayment(l.ID, l.UserName, connStr);
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
            Login l = _loginDAO.GetLogin(0, username, "", connStr);
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
