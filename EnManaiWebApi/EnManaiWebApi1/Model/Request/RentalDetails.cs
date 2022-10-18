namespace EnManaiWebApi.Model.Request
{
    public class RentalDetails
    {
        public HouseOwner houseOwner { get; set; }
        public HouseOwnerResidingAddress residingAddress { get; set; }
        public List<RentalHouseDetail> rentalHouseDetails { get; set; }
    }
}
