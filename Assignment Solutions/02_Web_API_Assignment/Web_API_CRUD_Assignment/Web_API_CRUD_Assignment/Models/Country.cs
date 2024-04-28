using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API_CRUD_Assignment.Models
{
    public class Country
    {
 
         //Adding Properties in the country model as ID, CountryName, Capital
        
        public int ID { get; set; }
        public string CountryName { get; set; }
        public string Capital { get; set; }
    }
}