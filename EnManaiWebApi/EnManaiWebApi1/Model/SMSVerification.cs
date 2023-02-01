namespace EnManaiWebApi.Model
{
    public class SMSVerification
    {
        public string ID { get; set; }
        public string Username{ get; set; }
        public string LoginId { get; set; }
        public string Phonenumber { get; set; }
        public DateTime SMSSendDateTime { get; set; }
        public DateTime SMSExpiryDateTime { get; set; }
        public string? Status { get; set; }
        public int NoOfVerificationPerDay { get; set; }
        public int BlockForDays { get; set; }
        public int BlockAfterAttempt { get; set; }
        public int TotalNoAttempt { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string SMSCode { get; set; }
        public string LastSMSSendStatus { get; set; }
    }
}
