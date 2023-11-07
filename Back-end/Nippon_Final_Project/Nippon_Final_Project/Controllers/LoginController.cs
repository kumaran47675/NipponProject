using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nippon_Final_Project.Models;
using System.Linq;

namespace Nippon_Final_Project.Controllers
{
    [ApiController]
    [EnableCors("corspolicy")]
    public class LoginController : ControllerBase
    {
        private TintingRecordsContext trc;
        public LoginController(TintingRecordsContext trc)
        {
            this.trc = trc;
        }

        [HttpGet]
        [Route("api/login/get/{UserId}")]
        public List<MergedRecord> GetUserId(string UserId)
        {
           

            if (UserId != null)
            {
                var records1 = trc.MappingMasters.Where(x => x.UserId == UserId).Select(x => new MergedRecord
                {
                    UserName = x.UserName,
                    Depot=x.DepotName,
                    Admin=x.Admin
                }).ToList();
                var records = trc.LoginPages.Where(x => x.UserId == UserId).Select(x => new MergedRecord
                {
                    UserId = x.UserId,
                    Password = x.Password,
                    UserName = records1[0].UserName,
                    Depot = records1[0].Depot,
                    Admin = records1[0].Admin
                }).ToList();

                return records;



            }
            else
            {
                var nullRecord = new MergedRecord()
                {
                    UserId = null,
                    Password = null,
                    UserName=null
                };

                return new List<MergedRecord> { nullRecord };
            }


        }
    }

   
}
