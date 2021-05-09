using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMVC_CODIV2021.BusinessService;
using WebMVC_CODIV2021.DTO;
using WebMVC_CODIV2021.Utilities;

namespace WebMVC_CODIV2021.Controllers
{
    public class CovidInfoController : Controller
    {
        // GET: CovidInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCovidCases()
        {
            return View();

        }
        [HttpPost]
        public async Task<ActionResult> GetAllCovidCases()
        {
            Services services = new Services();
            string newFilter = String.Empty;//"date=2020-04-16";
            Data result = await services.ConsumeAPI(newFilter);
            //Get the Most COVID cases 
            var response = result.data.OrderByDescending(c=>c.confirmed).Take(10).ToList();
            return Json(response.GetCompressDataTotal(), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public async Task<ActionResult> GetCovidCasesByFilter(string filter)
        {
            Services services = new Services();
            string newFilter = string.Empty;
            newFilter = string.Format("iso={0}", filter);
            Data result = await services.ConsumeAPI(newFilter);
            //Get the Most COVID cases 
            var response = result.data.OrderByDescending(c => c.confirmed).Take(10).ToList();
            return Json(response.GetCompressData(), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public async Task<ActionResult> GetRegions()
        {
            Services services = new Services();

            Result result = await services.GetRegions();
            //Get the Regions 
            var response = result.data.OrderBy(n=>n.Name).ToList();
            return Json(response, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public async Task<ActionResult> ExportXML(string filter)
        {
            Services services = new Services();
            Data result = await services.ConsumeAPI(string.Format("iso={0}", filter));
            //Get the Most COVID cases 
            List<Cases> data  = result.data.OrderByDescending(c => c.confirmed).Take(10).ToList();
            var ms = Util.GetDataXML(data);

            return File(ms, "text/xml", string.Format("SampleCovid19_XML{0}.xml", DateTime.Now.ToString("yyyyMMddHHmm")));

        }
        [HttpGet]
        public async Task<ActionResult> ExportJSON(string filter)
        {
            Services services = new Services();
            Data result = await services.ConsumeAPI(string.Format("iso={0}", filter));
            //Get the Most COVID cases 
            List<Cases> data = result.data.OrderByDescending(c => c.confirmed).Take(10).ToList();
            var download = Util.GetDataJSON(data, new JsonSerializerSettings());
  

            return File(download, "application/json", string.Format("SampleCovid19_JSON{0}.json", DateTime.Now.ToString("yyyyMMddHHmm")));

        }
     
        [HttpGet]
        public async Task<ActionResult> ExportCSV(string filter)
        {
            Services services = new Services();
            Data result = await services.ConsumeAPI(string.Format("iso={0}", filter));
            //Get the Most COVID cases 
            List<Cases> data = result.data.OrderByDescending(c => c.confirmed).Take(10).ToList();
            var download = Util.GetDataCSV(data);

            return File(download, "text/csv", string.Format("SampleCovid19_CSV{0}.csv", DateTime.Now.ToString("yyyyMMddHHmm")));
        }
    }
}