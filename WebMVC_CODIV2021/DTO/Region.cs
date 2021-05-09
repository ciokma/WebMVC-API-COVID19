
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC_CODIV2021.DTO
{
    /// <summary>
    /// List of Regions
    /// </summary>
    public class Result
    {
        public List<RegionDto> data { get; set; }
    }
    /// <summary>
    /// Region
    /// </summary>
    public class RegionDto
    {
        public string iso { get; set; }
        public string Name { get; set; }
    }
}