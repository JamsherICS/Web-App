using System;
using System.Collections.Generic;

namespace _08_MAY_MVC_DB_First_Test.Models;

public partial class MigrationHistory
{
    public string MigrationId { get; set; } = null!;

    public string ContextKey { get; set; } = null!;

    public byte[] Model { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
