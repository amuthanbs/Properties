namespace EnManaiWebApi.Model.Request
{
    public class CommonRentalDetails
    {
        public HouseOwner houseOwner { get; set; }
        public HouseOwnerResidingAddress residingAddress { get; set; }
    }
    public class RentalDetails :CommonRentalDetails
    {
        public List<RentalHouseDetail> rentalHouseDetails { get; set; }
    }

    // Unregistered 
    public class UnregisteredRentalDetails : CommonRentalDetails
    {
        public List<UnregisteredRentalHouseDetail> rentalHouseDetails { get; set; }
    }
}
