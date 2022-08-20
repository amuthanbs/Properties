using EnManaiWebApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnManaiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouserOwnerController : ControllerBase
    {
        public HouserOwnerController()
        {

        }

        [HttpGet,Route("GetAll")]
        public IActionResult GetAllHouserOwner()
        {
            HouseOwnersResponse houseOwnersResponse = new HouseOwnersResponse();
            try
            {
                return Ok(houseOwnersResponse);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
