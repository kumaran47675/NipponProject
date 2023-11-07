using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nippon_Final_Project.Models;

namespace Nippon_Final_Project.Controllers
{
    [ApiController]
    [EnableCors("corspolicy")]
    public class TintingsController : ControllerBase
    {

        private TintingRecordsContext trc;
       
        public TintingsController(TintingRecordsContext trc)
        {
            this.trc = trc;
           
        }

        [HttpPost]
        [Route("api/tintings/post")]
        public async Task<ActionResult<string>> AddContact([FromBody] TintingsRequest tintings)
        {

            string Nameofsalesperson = null;
            if (tintings.NameOfSalesPerson != null)
            {
                Nameofsalesperson = tintings.NameOfSalesPerson;
            }

            string requestNumber = GenerateRequestNumber1();

            string Colourshade = null;
            if (tintings.ColourShade != null)
            {
                Colourshade = tintings.ColourShade;
            }

            string Forprojectorretail = null;
            if (tintings.ForProjectOrRetail != null)
            {
                Forprojectorretail = tintings.ForProjectOrRetail;
            }

            string Forsmprojectorretailorbuka = null;
            if (tintings.ForSMProjectOrRetailOrBuka != null)
            {
                Forsmprojectorretailorbuka = tintings.ForSMProjectOrRetailOrBuka;
            }

            string Dispensingmachine = null;
            if(tintings.DispensingMachine!=null)
            {
                Dispensingmachine= tintings.DispensingMachine;
            }

            string tintinginvoice = null;
            if (tintings.TintingInvoice != null)
            {
                tintinginvoice = tintings.TintingInvoice;
            }

            string originalinvoice = null;
            if (tintings.OriginalInvoice != null)
            {
                originalinvoice = tintings.OriginalInvoice;
            }




            Tinting ob = new Tinting
            {
                UserId=tintings.UserId,
                OriginalInvoice=originalinvoice,
                TintingInvoice=tintinginvoice,
                DispensingMachine=Dispensingmachine,
                ForProjectOrRetail=Forprojectorretail,
                ForSMProjectOrRetailOrBuka=Forsmprojectorretailorbuka,
                ColourShade=Colourshade,
                NameOfSalesPerson=Nameofsalesperson,
                CreatedOn=DateTime.Now.ToUniversalTime(),
                Status=1,
                RequestId=requestNumber,
                Date=tintings.Date,
                NameOfTheProject=tintings.NameOfTheProject,
                Fandeck=tintings.Fandeck,
                ShadeCode=tintings.ShadeCode,
                ShadeName=tintings.ShadeName,
                Product=tintings.Product,
                Base=tintings.Base,
                BaseBatchNo=tintings.BaseBatchNo,   
                FormulationFor1LitrePackSize=tintings.FormulationFor1LitrePackSize,
                QuantityTintedInLitres =tintings.QuantityTintedInLitres,
                Reference=tintings.Reference,
                ShadeMatchConfirmation=tintings.ShadeMatchConfirmation,
                ShadePatch=tintings.ShadePatch,
                OtherObservations=tintings.OtherObservations
            };

            trc.Tintings.Add(ob);
            trc.SaveChanges();
            return requestNumber;
        }


        [HttpGet]
        [Route("api/tintings/get")]
        public List<Tinting> GetDetails()
        {

            var records = trc.Tintings.Select(x => new Tinting()
            {
               RequestId=x.RequestId,
               Date=x.Date,
               Status=x.Status,
               NameOfTheProject=x.NameOfTheProject,
               NameOfSalesPerson=x.NameOfSalesPerson,
               Fandeck = x.Fandeck,
               ShadeCode = x.ShadeCode,
               ColourShade = x.ColourShade,
               ShadeName = x.ShadeName,
               Product = x.Product,
               Base = x.Base,
               BaseBatchNo = x.BaseBatchNo,
               FormulationFor1LitrePackSize=x.FormulationFor1LitrePackSize,
               QuantityTintedInLitres = x.QuantityTintedInLitres,
               Reference=x.Reference,
               ForProjectOrRetail = x.ForProjectOrRetail,
               ForSMProjectOrRetailOrBuka = x.ForSMProjectOrRetailOrBuka,
               ShadeMatchConfirmation=x.ShadeMatchConfirmation,
               ShadePatch = x.ShadePatch,
               OtherObservations = x.OtherObservations,
               DispensingMachine = x.DispensingMachine,
               TintingInvoice = x.TintingInvoice,
               OriginalInvoice=x.OriginalInvoice,
               UserId=x.UserId


            }).ToList();

            return records;


        }


        [HttpPut]
        [Route("api/tinting/status/{requestId}")]
        public void Statuschange(string requestId)
        {
            var tintingsToChangeStatus = trc.Tintings.Where(x => x.RequestId == requestId);

            foreach (var tinting in tintingsToChangeStatus)
            {
                tinting.Status = 3;

            }
            trc.SaveChanges();

        }

        private string GenerateRequestNumber1()
        {
            var requestGenerator1 = trc.RequestIdGeneratorTintings.FirstOrDefault(x => x.Sno == 1);

            DateTime lastRequestDate1 = requestGenerator1.TintingRequestIdDate.ToUniversalTime();
            int lastRequestNumber1 = requestGenerator1.TintingRequestIdNumber;

            if (lastRequestDate1.Date != DateTime.UtcNow.Date)
            {
                lastRequestDate1 = DateTime.UtcNow.Date;
                lastRequestNumber1 = 1;

                requestGenerator1.TintingRequestIdDate = lastRequestDate1;
                requestGenerator1.TintingRequestIdNumber = lastRequestNumber1;
                trc.SaveChanges();
            }
            else
            {
                lastRequestNumber1++;
            }

            requestGenerator1.TintingRequestIdNumber = lastRequestNumber1;
            trc.SaveChanges();

            if (lastRequestNumber1 <= 999)
            {
                return $"{lastRequestDate1.ToString("ddMMyy")}{lastRequestNumber1:D3}";
            }
            else if (lastRequestNumber1 >= 1000 && lastRequestNumber1 <= 9999)
            {
                return $"{lastRequestDate1.ToString("ddMMyy")}{lastRequestNumber1:D4}";
            }
            else if (lastRequestNumber1 >= 10000 && lastRequestNumber1 <= 99999)
            {
                return $"{lastRequestDate1.ToString("ddMMyy")}{lastRequestNumber1:D5}";
            }
            else
            {
                return $"{lastRequestDate1.ToString("ddMMyy")}{lastRequestNumber1:D6}";
            }
        }





    }
}
