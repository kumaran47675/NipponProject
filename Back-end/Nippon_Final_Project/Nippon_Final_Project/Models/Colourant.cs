using System;
using System.Collections.Generic;

namespace Nippon_Final_Project.Models;

public partial class Colourant
{
    public int Sno { get; set; }

    public string RequestId { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? DispensingMachine { get; set; }

    public string Colourants { get; set; } = null!;

    public int BatchNo { get; set; }

    public int Quantity { get; set; }

    public string UserId { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string? Mfg { get; set; }
}
