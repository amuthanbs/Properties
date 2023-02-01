using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace EnManaiWebApi.Model.Request
{
    public class SMSVerificationRequest
    {
        //public string ID { get; set; }
        public string Username { get; set; }
        public string LoginId { get; set; }
        public string Phonenumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class SMSCodeVerificationRequest : SMSVerificationRequest
    {
        public string SMSCode { get; set; }
    }
    public class SMSVerificationNewOrUpdateRequest: SMSVerificationRequest
    {
        public DateTime SMSSendDateTime { get; set; }
        public DateTime SMSExpiryDateTime { get; set; }
        public string SMSCode { get; set; }
    }
    public class SMSCodeRequest
    {
        public int SMSCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public int LoginId { get; set; }
        public string Username { get; set; }
    }
}
