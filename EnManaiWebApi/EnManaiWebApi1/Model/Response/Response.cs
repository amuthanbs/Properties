using EnManaiWebApi.Model.Request;

namespace EnManaiWebApi.Model.Response
{
    public enum Status
    {
        Success,
        DataError,
        InternalError
    }
    public class Response
    {
        public Status status { get; set; }
        public string? ErrorMessage { get; set; }
        public bool paidUser { get; set; } = false;
        public bool paidExpiredUser { get; set; } = false;
        public bool Logged { get; set; } = false;

        
    }
    public class RhdPhoneNumberResponse:Response
    {
        public RentalHouseDetailPhoneNumber RentalHouseDetailPhoneNumber { get; set; } 
    }
}
