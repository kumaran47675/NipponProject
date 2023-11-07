namespace Nippon_Final_Project.Models
{
    public class ObservationRequest
    {
        public int BaseTintedAsPerReportInLit { get; set; }

        public int BaseTintedAsPerHistoryFileInLit { get; set; }

        public int ColorantPouredInCannistersInLit { get; set; }

        public int ColorantConsumedInLit { get; set; }

        public string? Remarks { get; set; }

        public string BrandingForDispensingMachine { get; set; } = null!;

        public string BrandingForGyroshakerMachine { get; set; } = null!;
        
        public string Depot { get; set; } = null!;

        public string UserId { get; set; }

    }
}
