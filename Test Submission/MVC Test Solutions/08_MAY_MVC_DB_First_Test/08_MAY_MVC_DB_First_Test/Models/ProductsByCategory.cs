using System;
using System.Collections.Generic;

namespace _08_MAY_MVC_DB_First_Test.Models;

public partial class ProductsByCategory
{
    public string CategoryName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? QuantityPerUnit { get; set; }

    public short? UnitsInStock { get; set; }

    public bool Discontinued { get; set; }
}
