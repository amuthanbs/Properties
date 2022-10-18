namespace EnManaiWebApi.Model
{
    public class HouseOwnerResidingAddress
    {
        public int Id { get; set; }
        public int HouseOwnerID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CIty { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public char Pincode { get; set; }
    }
}
