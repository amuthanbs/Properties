using EnManaiWebApi.Model.Request;

namespace EnManaiWebApi.Model.Response
{
    
    public class HouseOwnersResponse : Response
    {
        List<HouseOwner> houseOwners { get; set; }
    }
}
