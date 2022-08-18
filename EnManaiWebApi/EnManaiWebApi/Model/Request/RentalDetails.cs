namespace EnManaiWebApi.Model.Request
{
    public class RentalDetails
    {
        public HouseOwner houseOwner { get; set; }
        public HouseOwnerResidingAddress residingAddress {get;set;}
        public RentalHouseDetails rentalHouseDetails { get; set; }
    }
}
