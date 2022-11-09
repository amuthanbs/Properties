using EnManaiWebApi.DAO;
using EnManaiWebApi.Model.Request;
using System.Drawing.Printing;
using TokenBased.Model;

namespace EnManaiWebApi.Model.Response
{
    public class LoginResponse:Response
    {
        public List<Login> logins { get; set; }
        public PaymentForTenant paymentForTenant { get; set; }
        public UserTokens accessToken { get; set; }
    }
}
