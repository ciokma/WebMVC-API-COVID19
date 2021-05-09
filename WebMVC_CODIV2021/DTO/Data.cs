using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC_CODIV2021.DTO
{
   /// <summary>
   /// List of cases
   /// </summary>
    public class Data
    {
        public List<Cases> data { get; set; }
    }
    /// <summary>
    /// Cases
    /// </summary>
    public class Cases
    {
        public DateTime date { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
        public int confirmed_diff { get; set; }
        public int recovered_diff { get; set; }
        public DateTime last_update { get; set; }
        public int active { get; set; }
        public float fatality_rate { get; set; }
        public Region region { get; set; }

    }
    /// <summary>
    /// Cases by region
    /// </summary>
    public class Region
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public float lat { get; set; }
        public float _long { get; set; }
        public List<City> cities  { get; set; }
    }
    /// <summary>
    /// Cases by city
    /// </summary>
    public class City
    {
        public string name { get; set; }
        public DateTime date { get; set; }
        public int fips { get; set; }
        public float lat { get; set; }
        public float _long { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public DateTime last_update { get; set; }
    }
}