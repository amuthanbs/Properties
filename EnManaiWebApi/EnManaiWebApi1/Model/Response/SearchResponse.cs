using EnManaiWebApi.Model.Request;

namespace EnManaiWebApi.Model.Response
{
    public class SearchResponse:Response
    {
        public List<RentalHouseDetail> rentalDetails { get; set; }
        public List<HouseOwner> houseOwners { get; set; }
    }
}
