using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace _2_Code_first_Approach.Models
{
    public class MovieContext:DbContext
    {
        public MovieContext() : base("connectstr")
        { }
        public DbSet<Movies> Product { get; set; }
    }
}