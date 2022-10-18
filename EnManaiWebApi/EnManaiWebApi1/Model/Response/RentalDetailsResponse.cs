using EnManaiWebApi.Model.Request;

namespace EnManaiWebApi.Model.Response
{
    public class RentalDetailsResponse : Response
    {
        public RentalDetails rentalDetails { get; set; }
    }

    public class UpdateResponse : Response
    {
        public int Count { get; set; }
    }
}
