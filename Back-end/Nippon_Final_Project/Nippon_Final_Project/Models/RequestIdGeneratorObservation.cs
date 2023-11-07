using System;
using System.Collections.Generic;

namespace Nippon_Final_Project.Models;

public partial class RequestIdGeneratorObservation
{
    public int Sno { get; set; }

    public DateTime ObservationRequestIdDate { get; set; }

    public int ObservationRequestIdNumber { get; set; }
}
