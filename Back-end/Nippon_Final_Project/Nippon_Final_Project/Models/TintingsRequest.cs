namespace Nippon_Final_Project.Models
{
    public class TintingsRequest
    {

        public string UserId { get; set; }

        public string Date { get; set; } = null!;

        public string NameOfTheProject { get; set; } = null!;

        public string? NameOfSalesPerson { get; set; }

        public string Fandeck { get; set; } = null!;

        public string ShadeCode { get; set; } = null!;

        public string? ColourShade { get; set; }

        public string ShadeName { get; set; } = null!;

        public string Product { get; set; } = null!;

        public string Base { get; set; } = null!;

        public string BaseBatchNo { get; set; } = null!;

        public string FormulationFor1LitrePackSize { get; set; } = null!;

        public int QuantityTintedInLitres { get; set; }

        public string Reference { get; set; } = null!;

        public string? ForProjectOrRetail { get; set; }

        public string? ForSMProjectOrRetailOrBuka { get; set; }

        public bool ShadeMatchConfirmation { get; set; } 

        public string ShadePatch { get; set; } = null!;

        public string OtherObservations { get; set; } = null!;

        public string? DispensingMachine { get; set; }

        public string? TintingInvoice { get; set; }

        public string? OriginalInvoice { get; set; }
    }
}
