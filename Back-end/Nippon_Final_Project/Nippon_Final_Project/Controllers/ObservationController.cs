using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nippon_Final_Project.Models;

namespace Nippon_Final_Project.Controllers
{

    [ApiController]
    [EnableCors("corspolicy")]

    public class ObservationController : ControllerBase
    {
        private TintingRecordsContext trc;
        

        public ObservationController(TintingRecordsContext trc)
        {
            this.trc = trc;
           
        }

        [HttpPost]
        [Route("api/observation/post")]
        public async void AddContact([FromBody] ObservationRequest observation)
        {


            string Remarks = null;
            if (observation.Remarks != null)
            {
               Remarks = observation.Remarks;
            }
            string requestNumber = GenerateRequestNumber();
            Observation ob = new Observation
            {

                Status = 1,
                RequestId =requestNumber,
                CreatedOn = DateTime.Now.ToUniversalTime(),
                UserId = observation.UserId,
                Depot = observation.Depot,
                Remarks = Remarks,
                BaseTintedAsPerReportInLit=observation.BaseTintedAsPerReportInLit,
                BaseTintedAsPerHistoryFileInLit=observation.BaseTintedAsPerHistoryFileInLit,
                ColorantPouredInCannistersInLit=observation.ColorantPouredInCannistersInLit,
                ColorantConsumedInLit = observation.ColorantConsumedInLit,
                BrandingForDispensingMachine = observation.BrandingForDispensingMachine,
                BrandingForGyroshakerMachine = observation.BrandingForGyroshakerMachine
            };
            trc.Observations.Add(ob);
            trc.SaveChanges();
        }

        [HttpGet]
        [Route("api/observation/get")]
        public List<Observation> GetDetails()
        {

            var records = trc.Observations.Select(x => new Observation()
            {
               BaseTintedAsPerReportInLit=x.BaseTintedAsPerReportInLit,
               BaseTintedAsPerHistoryFileInLit=x.BaseTintedAsPerHistoryFileInLit,
               ColorantConsumedInLit=x.ColorantConsumedInLit,   
               ColorantPouredInCannistersInLit=x.ColorantPouredInCannistersInLit,
               Depot=x.Depot,
               Remarks=x.Remarks,
               BrandingForDispensingMachine=x.BrandingForDispensingMachine,
               BrandingForGyroshakerMachine = x.BrandingForGyroshakerMachine,
               Status = x.Status,
               RequestId=x.RequestId

            }).ToList();

            return records;


        }



        private string GenerateRequestNumber()
        {
            var requestGenerator = trc.RequestIdGeneratorObservations.FirstOrDefault(x => x.Sno == 1);

            DateTime lastRequestDate = requestGenerator.ObservationRequestIdDate.ToUniversalTime();
            int lastRequestNumber = requestGenerator.ObservationRequestIdNumber;

            if (lastRequestDate.Date != DateTime.UtcNow.Date)
            {
                lastRequestDate = DateTime.UtcNow.Date;
                lastRequestNumber = 1;

                requestGenerator.ObservationRequestIdDate = lastRequestDate;
                requestGenerator.ObservationRequestIdNumber = lastRequestNumber;
                trc.SaveChanges();
            }
            else
            {
                lastRequestNumber++;
            }

            requestGenerator.ObservationRequestIdNumber = lastRequestNumber;
            trc.SaveChanges();

            if (lastRequestNumber <= 999)
            {
                return $"{lastRequestDate.ToString("ddMMyy")}{lastRequestNumber:D3}";
            }
            else if (lastRequestNumber >= 1000 && lastRequestNumber <= 9999)
            {
                return $"{lastRequestDate.ToString("ddMMyy")}{lastRequestNumber:D4}";
            }
            else if (lastRequestNumber >= 10000 && lastRequestNumber <= 99999)
            {
                return $"{lastRequestDate.ToString("ddMMyy")}{lastRequestNumber:D5}";
            }
            else
            {
                return $"{lastRequestDate.ToString("ddMMyy")}{lastRequestNumber:D6}";
            }
        }


    }
}
