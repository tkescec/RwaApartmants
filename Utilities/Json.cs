using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Utilities
{
    public static class JsonHelper
    {
        public static string ToJson(object data) //n toJS()
        {
            var JSON = GetSerializer().Serialize(data);
            return JSON;
        }
        public static T FromJS<T>(string data) //n fromJS()
        {
            return GetSerializer().Deserialize<T>(data);
        }

        public static JavaScriptSerializer GetSerializer()
        {
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            return serializer;
        }

        public static string ToJsonNewtonsoft(object data)
        {
            return JsonConvert.SerializeObject(data);
        }


        public static T FromJsonNewtonsoft<T>(string data) //n fromJS()
        {
            return JsonConvert.DeserializeObject<T>(data);
        }


        /// <summary>
        /// converts datetime to dd.mm.yyyy hh:MM:ss
        /// </summary>
        public class DateTimeToCroatianFormat : IsoDateTimeConverter
        {
            public DateTimeToCroatianFormat()
            {
                base.DateTimeFormat = "dd.MM.yyyy hh:MM:ss";
            }
        }

        /// <summary>
        /// converts datetime to yyyy-MM-dd hh:MM:ss
        /// </summary>
        public class DateTimeToStandard : IsoDateTimeConverter
        {
            public DateTimeToStandard()
            {
                base.DateTimeFormat = "yyyy-MM-dd hh:MM:ss";
            }
        }
    }
}
