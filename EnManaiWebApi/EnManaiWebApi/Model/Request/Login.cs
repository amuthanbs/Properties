namespace EnManaiWebApi.Model.Request
{
    public class Login
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? HouseOwnerId { get; set; }
        public int? TenantId { get; set; }
        public string PhoneNumber { get; set; }
        public string EMailId { get; set; }
        public bool PhoneNumberVerified { get; set; }
        public bool EmailVerified { get; set; }
        public int ReverficationTime { get; set; }
        public DateTime? PhoneNumberVerifiedDate { get; set; }
        public DateTime? EmailIdVerifiedDate { get; set; }
        public bool MandatoryVerification { get; set; }
        public bool ReVerification { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
