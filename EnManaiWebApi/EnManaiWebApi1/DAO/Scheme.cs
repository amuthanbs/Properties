namespace EnManaiWebApi.DAO
{
    public class Scheme
    {
        public int Id { get; set; }
        public string SchemeName { get; set; }
        public int SchemeDuration { get; set; }
        public int DiscountPercentage { get; set; }
        public int Amount { get; set; }
        public bool Rental { get; set; }
        public bool Tenant { get; set; }
    }
}
