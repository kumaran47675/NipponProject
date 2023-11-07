namespace Nippon_Final_Project.Models
{
    public class ColourantsRequest
    {
       

        public string Colourants { get; set; } = null!;

        public int BatchNo { get; set; }

        public int Quantity { get; set; }

        public string Date { get; set; } = null!;

        public string? Mfg { get; set; }

        public string? DispensingMachine { get; set; }

        public string UserId { get; set; }

        public string RequestId { get; set; } = null!;







    }
}
