using EnManaiWebApi.Model.Request;

namespace EnManaiWebApi.Model.Response
{
    public class LoginResponse:Response
    {
        public List<Login> logins { get; set; }
    }
}
