using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nippon_Final_Project.Models;

namespace Nippon_Final_Project.Controllers
{
    [ApiController]
    [EnableCors("corspolicy")]
    public class ColourantsController : ControllerBase
    {
        private TintingRecordsContext trc;
      
        public ColourantsController(TintingRecordsContext trc)
        {
            this.trc = trc;
            
        }

        [HttpPost]
        [Route("api/colourants/post")]
        public async void AddContact([FromBody] ColourantsRequest colourants)
        {

           
            string mfg = null;
            if (colourants.Mfg != null)
            {
                mfg = colourants.Mfg;
            }
            string Dispensingmachine = null;
            if(colourants.DispensingMachine != null)
            {
                Dispensingmachine=colourants.DispensingMachine;
            }
            string requestId =colourants.RequestId;

            Colourant ob = new Colourant()
            {
               
                Colourants = colourants.Colourants,
                BatchNo = colourants.BatchNo,
                Quantity = colourants.Quantity,
                DispensingMachine = Dispensingmachine,
                Status = 1,
                Date = colourants.Date,
                Mfg = mfg,
                RequestId = colourants.RequestId,
                CreatedOn = DateTime.Now.ToUniversalTime(),
                UserId =colourants.UserId,
               
            };
            var tintingsToChangeStatus = trc.Tintings.Where(x => x.RequestId == requestId);

            foreach (var tinting in tintingsToChangeStatus)
            {
                tinting.Status = 2;

            }


            trc.Colourants.Add(ob);
            trc.SaveChanges();
        }


       




    }
}
