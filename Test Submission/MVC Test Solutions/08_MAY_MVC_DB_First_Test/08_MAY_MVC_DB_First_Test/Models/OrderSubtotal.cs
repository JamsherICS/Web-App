﻿using System;
using System.Collections.Generic;

namespace _08_MAY_MVC_DB_First_Test.Models;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
