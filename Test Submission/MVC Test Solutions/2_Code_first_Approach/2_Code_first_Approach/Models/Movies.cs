using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2_Code_first_Approach.Models
{
    public class Movies
    {
        [Key]
        public int MId { get; set; }
        public string MName { get; set; }

        public DateTime DateofRelease { get; set; }
    }
}