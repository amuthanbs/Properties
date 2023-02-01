using EnManaiWebApi.DAO;
using EnManaiWebApi.Model;
using EnManaiWebApi.Model.Request;
using EnManaiWebApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using RestSharp;
using System.Runtime.CompilerServices;
using TokenBased.Model;

namespace EnManaiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly ISMSVerificationDAO _iSMSVerificationDAO;
        private ILogger<VerificationController> _logger;
        private ILoginDAO _login;
        private readonly IConfiguration _config;
        private readonly string connStr;
        private readonly int BlockAfterAttempt;
        private readonly int BlockForDays;
        private readonly int SMSExpiryDateTimeInMinutes;
        public VerificationController(
            ISMSVerificationDAO iSMSVerificationDAO,
             IConfiguration config,
             ILogger<VerificationController> logger,
             ILoginDAO login)
        {
            _iSMSVerificationDAO = iSMSVerificationDAO;
            _logger = logger;
            _config = config;
            connStr = _config.GetSection("ConnectionStrings:sql").Value;
            BlockAfterAttempt = int.Parse(_config.GetValue<string>("BlockAfterAttempt"));
            BlockForDays = int.Parse(_config.GetValue<string>("BlockForDays"));
            SMSExpiryDateTimeInMinutes = int.Parse(_config.GetValue<string>("SMSExpiryDateTimeInMinutes"));
            _login = login;
        }

        private RestResponse SendSMS(SMSCodeRequest smsCodeRequest)
        {
            //POST https://www.fast2sms.com/dev/bulkV2

            var client = new RestClient("https://www.fast2sms.com/dev/bulkV2");
            var request = new RestRequest("https://www.fast2sms.com/dev/bulkV2", RestSharp.Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("authorization", "H5NGfbh7T6us28AykUWtLmPDnZdlIc4KeQMwzOFBi31XxorCYaDQMynzOKdYe6rmgIP3HRZtVSU0uXE7");
            //request.AddParameter("message", "This is a test message from Amuthan");
            //request.AddParameter("language", "english");
            //request.AddParameter("route", "q");

            request.AddParameter("variables_values", smsCodeRequest.Message + " " + smsCodeRequest.SMSCode.ToString());
            request.AddParameter("route", "otp");
            request.AddParameter("numbers", smsCodeRequest.PhoneNumber);
            RestResponse response = client.Execute(request);
            return response;
        }

        [HttpPost, Route("CreateOrUpdateSMSVerification")]
        public IActionResult CreateOrUpdateSMSVerification(SMSVerificationNewOrUpdateRequest smsRequest)
        {
            string name = HttpContext.User.Identity.Name;
            //Login login = _login.getUserDetails(1, connStr);
            SMSVerification sms = new SMSVerification();
            sms.Username = smsRequest.Username;
            sms.LoginId = smsRequest.LoginId;
            sms.Phonenumber = smsRequest.Phonenumber;
            sms.CreatedOn = DateTime.Now;
            sms.CreatedBy = sms.Username;
            sms.ModifiedOn = DateTime.Now;
            sms.ModifiedBy = sms.Username;
            sms.SMSSendDateTime = smsRequest.SMSSendDateTime;
            sms.SMSCode = smsRequest.SMSCode;
            sms.SMSExpiryDateTime = smsRequest.SMSExpiryDateTime.AddMinutes(int.Parse(_config.GetValue<string>("SMSExpiryDateTimeInMinutes")));
            SMSVerificationResponse sMSVerificationResponse = new SMSVerificationResponse();
            try
            {
                _logger.LogInformation($" Input on CreateOrUpdateSMSVerification :{JsonConvert.SerializeObject(sms)}");
                SMSVerification sMSVerification = _iSMSVerificationDAO.GetSMSVerification(sms, connStr);
                sMSVerification = _iSMSVerificationDAO.CreateOrUpdateSMSVerification(sms, connStr, BlockAfterAttempt, BlockForDays);
                sMSVerificationResponse.sMSVerification = new SMSVerificationRequest();
                sMSVerificationResponse.sMSVerification.LoginId = sMSVerification.LoginId;
                sMSVerificationResponse.sMSVerification.Username = sMSVerification.Username;
                sMSVerificationResponse.sMSVerification.Phonenumber = sMSVerification.Phonenumber;
                sMSVerificationResponse.sMSVerification.CreatedOn = sMSVerification.CreatedOn;

                sMSVerificationResponse.status = Status.Success;
                _logger.LogInformation($" Call Success CreateOrUpdateSMSVerification :{JsonConvert.SerializeObject(sMSVerificationResponse)}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($" Error on CreateOrUpdateSMSVerification :{ex.Message}");
                sMSVerificationResponse.status = Status.DataError;
                sMSVerificationResponse.ErrorMessage = ex.Message;
            }
            return Ok(sMSVerificationResponse);
        }

        [HttpPost, Route("GetSMSVerfification")]
        public IActionResult GetSMSVerfification(SMSVerificationRequest smsRequest)
        {
            SMSVerification sms = new SMSVerification();
            sms.Username = smsRequest.Username;
            sms.LoginId = smsRequest.LoginId;
            sms.Phonenumber = smsRequest.Phonenumber;
            sms.CreatedOn = smsRequest.CreatedOn;
            //sms.CreatedOn = DateTime.Now;
            //sms.CreatedBy = smsRequest.Username;
            //sms.ModifiedOn = DateTime.Now;
            //sms.ModifiedBy = smsRequest.Username;

            SMSVerificationResponse sMSVerificationResponse = new SMSVerificationResponse();
            try
            {
                _logger.LogInformation($" Input on GetSMSVerfification :{JsonConvert.SerializeObject(sms)}");
                var sMSVerification = _iSMSVerificationDAO.GetSMSVerification(sms, connStr);

                sMSVerificationResponse.sMSVerification = new SMSVerificationRequest();
                sMSVerificationResponse.sMSVerification.LoginId = smsRequest.LoginId;
                sMSVerificationResponse.sMSVerification.Username = smsRequest.Username;
                sMSVerificationResponse.sMSVerification.Phonenumber = smsRequest.Phonenumber;
                sMSVerificationResponse.sMSVerification.CreatedOn = smsRequest.CreatedOn;
                if (sMSVerification != null)
                {
                    //sMSVerificationResponse.sMSVerification = new SMSVerificationRequest();
                    //sMSVerificationResponse.sMSVerification.LoginId = sMSVerification.LoginId;
                    //sMSVerificationResponse.sMSVerification.Username = sMSVerification.Username;
                    //sMSVerificationResponse.sMSVerification.Phonenumber = sMSVerification.Phonenumber;
                    sMSVerificationResponse.sMSVerification.CreatedOn = sMSVerification.CreatedOn;
                    if (sMSVerification.NoOfVerificationPerDay <= BlockAfterAttempt && sMSVerification.Status == "Active")
                    {
                        sMSVerificationResponse.VerificationOn = true;
                        //SMSCodeRequest smsrequest = new SMSCodeRequest();
                        //smsrequest.SMSCode = 1235;
                        //smsrequest.phonenumber = sMSVerification.Phonenumber;
                        //smsrequest.message = "Verfication code Send from EnManai By Amuthan";
                        //RestResponse res = SendSMS(smsrequest);
                    }
                    else
                    {
                        sMSVerificationResponse.status = Status.DataError;
                        sMSVerificationResponse.VerificationOn = false;
                        sMSVerificationResponse.ErrorMessage = "Verification Blocked";
                        _logger.LogInformation($" Call Failed GetSMSVerfification :{JsonConvert.SerializeObject(sMSVerificationResponse)}");
                        _iSMSVerificationDAO.UpdateSMSVerification(sMSVerification, connStr);
                        SMSCodeRequest smsrequest = new SMSCodeRequest();
                        //smsrequest.SMSCode = 1235;
                        //smsrequest.phonenumber = sMSVerification.Phonenumber;
                        //smsrequest.message = "user "+ sMSVerification.Phonenumber.ToString() + " is blocked for the day. By Amuthan";
                        //RestResponse res = SendSMS(smsrequest);
                    }
                    return Ok(sMSVerificationResponse);
                }
                //else
                //{
                //    sMSVerificationResponse.status = Status.DataError;
                //    sMSVerificationResponse.VerificationOn = false;
                //    sMSVerificationResponse.ErrorMessage = "No Veri";
                //    _logger.LogInformation($" Call Failed CreateOrUpdateSMSVerification :{JsonConvert.SerializeObject(sMSVerificationResponse)}");
                //    return Ok(sMSVerificationResponse);
                //}
                sMSVerificationResponse.VerificationOn = true;
                sMSVerificationResponse.status = Status.Success;
                _logger.LogInformation($" Call Success GetSMSVerfification :{JsonConvert.SerializeObject(sMSVerificationResponse)}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($" Error on GetSMSVerfification :{ex.Message}");
                sMSVerificationResponse.status = Status.DataError;
                sMSVerificationResponse.ErrorMessage = ex.Message;
            }
            return Ok(sMSVerificationResponse);
        }

        [HttpPost, Route("SendSMS")]
        public IActionResult SendVerificationSMS(SMSCodeVerificationRequest code)
        {
            Response response = new Response();
            try
            {
                code.SMSCode = 1254+"";
                response.status = Status.Success;
                //1. GetSMSVerfication

                //2. check noofvericationperday with configuration value.
                //3. if pass block for smsverification.
                //4. createorupdate sms and send success.

                //RestResponse res = SendSMS(code);
                //if (res.IsSuccessful)
                //{
                //    response.status = Status.Success;
                //}
                //else
                //{
                //    response.status = Status.InternalError;
                //}
                if (code != null)
                {
                    SMSVerification sms = _iSMSVerificationDAO.VerifySMSCode(code, connStr);
                    sms.SMSCode = code.SMSCode;
                    sms.SMSSendDateTime = DateTime.Now;
                    sms.SMSExpiryDateTime = sms.SMSSendDateTime.AddMinutes(SMSExpiryDateTimeInMinutes);
                    sms.CreatedOn = code.CreatedOn;
                    sms.CreatedBy = code.Username;
                    sms.ModifiedBy = code.Username;
                    sms.ModifiedOn = code.CreatedOn;
                    if ((sms != null) && (sms.NoOfVerificationPerDay <= BlockAfterAttempt)){
                        _iSMSVerificationDAO.CreateOrUpdateSMSVerification(sms, connStr, BlockAfterAttempt,BlockForDays);
                    }
                    else
                    {
                        response.ErrorMessage="SMS Verification Blocked for the day";
                        response.status = Status.DataError;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.status = Status.InternalError;
            }
            return Ok(response);
        }

        [HttpPost, Route("VerifySMSCode")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult VerifySendSMSCode(SMSCodeVerificationRequest verifySMSCode)
        {
            Response res = new Response();
            try
            {
                SMSVerification sms = null;
                if (verifySMSCode != null)
                {
                    sms = _iSMSVerificationDAO.VerifySMSCode(verifySMSCode, connStr);
                }
                if ((sms != null) && (sms.SMSCode == verifySMSCode.SMSCode) && (DateTime.Now <= sms.SMSExpiryDateTime))
                {
                    res.status = Status.Success;
                    try
                    {
                        Login l = _login.getUserDetails(Int32.Parse(verifySMSCode.LoginId), connStr);
                        l.PhoneNumberVerified = true;
                        l.PhoneNumberVerifiedDate = DateTime.Now;
                        l.ModifiedBy = verifySMSCode.Username;
                        l.ModifiedDate = DateTime.Now;
                        _login.updatePhoneVerificationStatus(l, connStr);
                    }
                    catch (Exception)
                    {
                        throw new Exception("update SMS Verfication on login table is failed");
                    }
                }
                else
                {
                    res.status = Status.InternalError;
                    res.ErrorMessage = "SMS Verification Failed By Amuthan";
                }
            }
            catch (Exception ex)
            {
                res.status = Status.InternalError;
                res.ErrorMessage = ex.Message;
            }
            return Ok(res);
        }
    }
}
