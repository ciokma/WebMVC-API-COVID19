using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using WebMVC_CODIV2021.DTO;

namespace WebMVC_CODIV2021.BusinessService
{
    /// <summary>
    /// Base Class to conect to an api
    /// </summary>
    public class Services
    {
        /// <summary>
        /// Consume api to get info related to covid19
        /// </summary>
        /// <param name="filter">filter: if filter is empty, get all result but get data through filter</param>
        /// <returns></returns>
        public async Task<Data> ConsumeAPI(string filter)
        {
            Data cases = new Data();
            string uri = string.Empty;
            var url = WebConfigurationManager.AppSettings["RequestUri"] == null ? string.Empty : WebConfigurationManager.AppSettings["RequestUri"];

            if (string.IsNullOrEmpty(filter))
            {
                uri = string.Format("{0}/reports", url);
            }
            else
            {
                uri = string.Format("{0}/reports?{1}", url, filter);
            }
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                var key = WebConfigurationManager.AppSettings["x-rapidapi-key"] == null ? string.Empty : WebConfigurationManager.AppSettings["x-rapidapi-key"];
                var host = WebConfigurationManager.AppSettings["x-rapidapi-host"] == null ? string.Empty : WebConfigurationManager.AppSettings["x-rapidapi-host"];

                request.RequestUri = new Uri(uri);
                request.Headers.Add("x-rapidapi-key", key);
                request.Headers.Add("x-rapidapi-host", host);
                using (var response = await client.SendAsync(request))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    body = body.Replace("long", "_long");
                    //setting for null values, in this case the cities can be null
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    cases = JsonConvert.DeserializeObject<Data>(body, settings);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return cases;
        }
       
        public async Task<Result> GetRegions()
        {
            Result regions = new Result();
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                var url = WebConfigurationManager.AppSettings["RequestUri"] == null ? string.Empty : WebConfigurationManager.AppSettings["RequestUri"];
                var key = WebConfigurationManager.AppSettings["x-rapidapi-key"] == null ? string.Empty : WebConfigurationManager.AppSettings["x-rapidapi-key"];
                var host = WebConfigurationManager.AppSettings["x-rapidapi-host"] == null ? string.Empty : WebConfigurationManager.AppSettings["x-rapidapi-host"];

                string uri = string.Format("{0}/regions",url);
                request.RequestUri = new Uri(uri);
                request.Headers.Add("x-rapidapi-key", key );
                request.Headers.Add("x-rapidapi-host", host);

                using (var response = await client.SendAsync(request))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    regions = JsonConvert.DeserializeObject<Result>(body, settings);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return regions;
        }
    }

}