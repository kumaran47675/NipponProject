using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nippon_Final_Project.Models;

namespace Nippon_Final_Project.Controllers
{
    public class MasterMappingController : ControllerBase
    {
        private TintingRecordsContext trc;
        public MasterMappingController(TintingRecordsContext trc) 
        {
            this.trc = trc;
        }


        [HttpGet]
        [Route("api/master/get/")]
        public List<MappingMaster> GetUser()
        {


                var records1 = trc.MappingMasters.Select(x => new MappingMaster
                {
                    Sno=x.Sno,
                    UserName = x.UserName,
                    DepotName = x.DepotName,
                    Admin=x.Admin,
                    Status = x.Status,
                    UserId = x.UserId,
                }).ToList();
             

                return records1;

        }


        



    }
}
