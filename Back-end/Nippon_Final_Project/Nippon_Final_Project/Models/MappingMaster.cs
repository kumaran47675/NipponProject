using System;
using System.Collections.Generic;

namespace Nippon_Final_Project.Models;

public partial class MappingMaster
{
    public int Sno { get; set; }

    public string UserName { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string DepotName { get; set; } = null!;

    public bool Admin { get; set; }

    public int Status { get; set; }
}
