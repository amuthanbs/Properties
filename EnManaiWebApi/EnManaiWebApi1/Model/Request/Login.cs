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
        public string EMailId { get; set; } = null;
        public bool PhoneNumberVerified { get; set; }
        public bool EmailVerified { get; set; }
        public int ReverficationTime { get; set; }
        public DateTime? PhoneNumberVerifiedDate { get; set; }
        public DateTime? EmailIdVerifiedDate { get; set; }
        public bool MandatoryVerification { get; set; }
        public bool ReVerification { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public DateTime ModifiedDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string ModifiedBy { get; set; }
        public int NonPaidedContactViewed { get; set; }
        public int PaidedContactViewed { get; set; }
        public bool Paided { get; set; }
        public int NoOfNonPaidedContact { get; set; }
        public string NonPaidContactList { get; set; }
    }
}
