using System;
using System.Collections.Generic;

namespace Nippon_Final_Project.Models;

public partial class Observation
{
    public string RequestId { get; set; } = null!;

    public int BaseTintedAsPerReportInLit { get; set; }

    public int BaseTintedAsPerHistoryFileInLit { get; set; }

    public int ColorantPouredInCannistersInLit { get; set; }

    public int ColorantConsumedInLit { get; set; }

    public string? Remarks { get; set; }

    public string BrandingForDispensingMachine { get; set; } = null!;

    public string BrandingForGyroshakerMachine { get; set; } = null!;

    public int Status { get; set; }

    public string Depot { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string UserId { get; set; } = null!;
}
