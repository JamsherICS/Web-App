using System;
using System.Collections.Generic;

namespace _08_MAY_MVC_DB_First_Test.Models;

public partial class TblBook
{
    public int BookId { get; set; }

    public string? BookName { get; set; }

    public double BookPrice { get; set; }
}
