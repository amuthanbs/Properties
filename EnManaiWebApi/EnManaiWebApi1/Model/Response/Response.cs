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
    }
}
