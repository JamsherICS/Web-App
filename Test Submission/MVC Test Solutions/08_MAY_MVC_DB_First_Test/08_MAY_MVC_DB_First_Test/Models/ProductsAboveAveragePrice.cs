using System;
using System.Collections.Generic;

namespace _08_MAY_MVC_DB_First_Test.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
