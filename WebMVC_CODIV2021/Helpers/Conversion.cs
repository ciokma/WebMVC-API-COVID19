using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebMVC_CODIV2021.Helpers
{
    public class Conversion
    {
        /// <summary>
        /// Serialize in xml the class 
        /// </summary>
        /// <typeparam name="T">Class</typeparam>
        /// <param name="dataToSerialize">dataToSerialize</param>
        /// <returns></returns>
        public static string Serialize<T>(T dataToSerialize)
        {
            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Deserialize data from string to object
        /// </summary>
        /// <typeparam name="T">object</typeparam>
        /// <param name="xmlText">xmlText</param>
        /// <returns></returns>

        public static T Deserialize<T>(string xmlText)
        {
            try
            {
                var stringReader = new System.IO.StringReader(xmlText);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
            catch
            {
                throw;
            }
        }
    }
}