using System;
using System.Collections.Generic;

namespace _08_MAY_MVC_DB_First_Test.Models;

public partial class SummaryOfSalesByYear
{
    public DateTime? ShippedDate { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
