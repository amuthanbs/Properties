namespace EnManaiWebApi.DAO
{
    public class PaymentForTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string UserName { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentReceivedDate { get; set; }
        public int AmountPaid { get; set; }
        public int SchemeId { get; set; }
        public string RefernceNumber { get; set; }
        public string BankDetails { get; set; }
        public bool TransactonSucessfull { get; set; }
        public DateTime PaymentExpiryDate { get; set; }
        public bool PaymentExpired { get; set; }
    }

}
