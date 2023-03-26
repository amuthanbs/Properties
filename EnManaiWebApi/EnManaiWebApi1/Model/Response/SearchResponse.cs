using EnManaiWebApi.Model.Request;

namespace EnManaiWebApi.Model.Response
{
    //Paid Response
    public class SearchResponse:Response
    {
        public List<RentalHouseDetail> rentalDetails { get; set; }
        public List<HouseOwner> houseOwners { get; set; }
        public bool paidUser { get; set; }=false;
    }
    // Logged Response
    public class LoggedSearchResponse : Response
    {
        public List<LoggedRentalHouseDetail> rentalDetails { get; set; }
        public List<HouseOwner> houseOwners { get; set; }
    }
    // Unregistered Response
    public class unregisteredSearchResponse : Response
    {
        public List<UnregisteredRentalHouseDetail> rentalDetails { get; set; }
        public List<HouseOwner> houseOwners { get; set; }
    }
    public class PaidedSearchResponse : Response
    {
        public List<PaidedRentalHouseDetail> rentalDetails { get; set; }
        public List<HouseOwner> houseOwners { get; set; }
    }
}
