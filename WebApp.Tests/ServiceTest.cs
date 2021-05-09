using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebMVC_CODIV2021.BusinessService;
using System.Threading.Tasks;
using WebMVC_CODIV2021.DTO;
using WebMVC_CODIV2021.Controllers;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebApp.Tests
{
    [TestFixture]
    public class ServiceTest
    {
        private Services _services;
        private string _filter;
        Data listCasesCodiv19 = new Data();

        [SetUp]
        public void SetUp()
        {
            _services = new Services();
            _filter = string.Empty;
            listCasesCodiv19 = new Data();
        }
        [Test]
        public async Task TestMethod_GetAllCases()
        {
            try
            {
                listCasesCodiv19 = await _services.ConsumeAPI(_filter);
                Assert.IsFalse(listCasesCodiv19.data.Count == 0, "there is not records");
            }
            catch
            {
                Assert.IsNull(listCasesCodiv19, "The service is not up - hheck connection");
            }
        }
        [Test]
        public async Task TestMethod_GetAllCasesByFilterISO_USA()
        {
            _filter = string.Format("iso={0}", "USA");
            try
            {
                var listCasesCodiv19ByFilter = await _services.ConsumeAPI(_filter);
                Assert.IsFalse(listCasesCodiv19ByFilter.data.Count == 0);
            }
            catch
            {
                Assert.IsNull(listCasesCodiv19, "The service is not up - check connection");
            }
        }
        [Test]
        public async Task TestMethod_GetAllRegion()
        {
            string filter = string.Empty;
            try
            {
                Result result = await _services.GetRegions();
                Assert.IsFalse(result.data.Count == 0);
            }
            catch
            {
                Assert.IsNull(listCasesCodiv19, "The service is not up - check connection");
            }

        }
     
    }
}
