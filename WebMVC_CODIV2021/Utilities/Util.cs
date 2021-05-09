using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using WebMVC_CODIV2021.DTO;
using WebMVC_CODIV2021.Helpers;

namespace WebMVC_CODIV2021.Utilities
{
    public static class  Util
    {
        /// <summary>
        /// Method that obtain the memorystream through list of cases model
        /// </summary>
        /// <param name="data">list of cases</param>
        /// <returns></returns>
        public static MemoryStream GetDataXML(List<Cases> data)
        {
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            var responseXml = Conversion.Serialize(data.GetCompressData());
            using (XmlWriter xw = XmlWriter.Create(ms, xws))
            {
                XDocument doc = XDocument.Parse(responseXml);
                doc.WriteTo(xw);
            }
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Get byte array to download file
        /// </summary>
        /// <param name="cases">list of cases</param>
        /// <param name="jsonSerializerSettings">jsonSerializerSettings</param>
        /// <returns></returns>
        public static byte[] GetDataJSON(List<Cases> cases, JsonSerializerSettings jsonSerializerSettings)
        {
          
            var result = JsonConvert.SerializeObject(cases.GetCompressData(), jsonSerializerSettings);

            return Encoding.UTF8.GetBytes(result);
        }
        /// <summary>
        /// Get byte array to download file
        /// </summary>
        /// <param name="cases">list of cases</param>
        /// <returns></returns>
        public static byte[] GetDataCSV(List<Cases> cases)
        {
            var compressData = cases.GetCompressDataCSV();

            //Insert the Column Names.
            compressData.Insert(0, new CompressDataCSV { province = "PROVINCE", confirmed = "CASES", deaths = "DEATH" });
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < compressData.Count; i++)
            {
                var comData = (CompressDataCSV)compressData[i];
                sb.Append(comData.province + ',');
                sb.Append(comData.confirmed.ToString() + ',');
                sb.Append(comData.deaths.ToString() );
                //Append new line character.
                sb.Append("\r\n");

            }
            var result = Encoding.UTF8.GetBytes(sb.ToString());
            return result ;
        }

        /// <summary>
        /// Get a minified version of the data when filtering
        /// </summary>
        /// <param name="cases">list of cases</param>
        /// <returns></returns>
        public static List<CompressData> GetCompressData(this List<Cases> cases)
        {
            List<CompressData> ListCompress = new List<CompressData>();
            cases.ForEach(r =>
            {
                CompressData compress = new CompressData()
                {
                    province = r.region.province,
                    deaths = r.deaths,
                    confirmed = r.confirmed
                };
                ListCompress.Add(compress);
            });
            return ListCompress;
        }
        /// <summary>
        /// Get a minified version of the data when load page
        /// </summary>
        /// <param name="cases">cases</param>
        /// <returns></returns>
        public static List<CompressDataTotal> GetCompressDataTotal(this List<Cases> cases)
        {

            List<CompressDataTotal> ListCompress = new List<CompressDataTotal>();
            cases.ForEach(r =>
            {
                CompressDataTotal compress = new CompressDataTotal()
                {
                    iso = r.region.iso,
                    deaths = r.deaths,
                    confirmed = r.confirmed
                };
                ListCompress.Add(compress);
            });
            return ListCompress;
        }
        /// <summary>
        /// Get a minified version of the data when is returned via csv
        /// </summary>
        /// <param name="cases"></param>
        /// <returns></returns>
        public static List<CompressDataCSV> GetCompressDataCSV(this List<Cases> cases)
        {
            List<CompressDataCSV> ListCompress = new List<CompressDataCSV>();
            cases.ForEach(r =>
            {
                CompressDataCSV compress = new CompressDataCSV()
                {
                    province = r.region.province,
                    deaths = r.deaths.ToString(),
                    confirmed = r.confirmed.ToString()
                };
                ListCompress.Add(compress);
            });
            return ListCompress;
        }
    }
}