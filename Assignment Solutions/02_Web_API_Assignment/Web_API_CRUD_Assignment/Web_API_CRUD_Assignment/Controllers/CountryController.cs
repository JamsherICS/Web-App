using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API_CRUD_Assignment.Models;

namespace Web_API_CRUD_Assignment.Controllers
{

    [RoutePrefix("api/User")]
    public class CountryController : ApiController
    {
        private static List<Country> countrylist = new List<Country>
        {
            new Country { ID = 1, CountryName = "India", Capital = "New Delhi" },
            new Country { ID = 2, CountryName = "Germany", Capital = "Berlin" },
            new Country { ID = 3, CountryName = "Poland", Capital = "Warsaw" },
            new Country { ID = 4, CountryName = "Indonesia", Capital = "Jakarta" },
            new Country { ID = 5, CountryName = "China", Capital = "Bijing" }
        };

        //get all countries
        [HttpGet]
        [Route("All")]
        public IEnumerable<Country> Get()
        {
            return countrylist;
        }

        //getting all countries using HttpResponseMessage
        [HttpGet]
        [Route("ByMassage")]
        public HttpResponseMessage GetAllCountry()
        {
            return Request.CreateResponse(HttpStatusCode.OK, countrylist);
        }

        //get country by ID using IHttpActionResult
        [HttpGet]
        [Route("ById/{Cid}")]
        public IHttpActionResult GetCountryById(int Cid)
        {
            var country = countrylist.Find(c => c.ID == Cid);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        //adding new country
        [HttpPost]
        [Route("AllPost")]
        public List<Country> PostAll([FromBody] Country country)
        {
            countrylist.Add(country);
            return countrylist;
        }

        //adding new country
        [HttpPost]
        [Route("CountryPost")]
        public void PersonPost([FromUri] int ID, string CountryName, string Capital)
        {
            Country country = new Country();
            country.ID = ID;
            country.CountryName = CountryName;
            country.Capital = Capital;
            countrylist.Add(country);
        }

        //update country
        [HttpPut]
        [Route("UpdateCountry")]
        public void CountryPut(int cid, [FromUri] Country country)
        {
            countrylist[cid - 1] = country;
        }

        //delete country
        [HttpDelete]
        [Route("DeleteCountry")]
        public void CountryDelete(int cid)
        {
            countrylist.RemoveAt(cid - 1);
        }
    }
}
