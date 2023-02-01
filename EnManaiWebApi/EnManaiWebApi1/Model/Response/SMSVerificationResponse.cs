using EnManaiWebApi.Model.Request;

namespace EnManaiWebApi.Model.Response
{
    //public class SMSVerificationResponse:Response
    //{
    //    public SMSVerification sMSVerification { get; set; }
    //    public bool VerificationOn { get; set; }
    //}

    public class SMSVerificationResponse : Response
    {
        public SMSVerificationRequest sMSVerification { get; set; }
        public bool VerificationOn { get; set; }
    }
}
