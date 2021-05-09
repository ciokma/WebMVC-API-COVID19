using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC_CODIV2021.DTO
{
    /// <summary>
    /// Class used to return minify data when query by filter
    /// </summary>
    public class CompressData
    {
        public string province { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }

    }
    /// <summary>
    /// Class used to minify data returned in grid at load the page
    /// </summary>
    public class CompressDataTotal
    {
        public string iso { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }

    }
    /// <summary>
    /// Class used to minify data to return en csv file
    /// </summary>
    public class CompressDataCSV
    {
        public string province { get; set; }
        public string confirmed { get; set; }
        public string deaths { get; set; }

    }
}